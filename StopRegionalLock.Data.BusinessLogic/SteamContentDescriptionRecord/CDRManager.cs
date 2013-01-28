using SteamKit2;
using SteamKit2.Blob;
using StopRegionalLock.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    public static class CDRManager
    {
        private const string CDR_FILENAME = "cdr.proto";

        public static CDR cdrObj;

        public static void Update()
        {
            typeof(CDRManager).Info("Updating CDR...");

            CDRConfig.Settings config = CDRConfig.Instance;

            string filename = ConfigurationManager.AppSettings["cdrFilename"] ?? CDR_FILENAME;
            filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string directory = Path.GetDirectoryName(filename);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var hasCachedCDR = File.Exists(filename);

            if (DateTime.Now > config.CDRCacheTime || !hasCachedCDR)
            {
                byte[] cdrHash = hasCachedCDR ? config.CDRHash : null;

                foreach (var configServer in config.ConfigServers)
                {
                    try
                    {
                        ConfigServerClient csClient = new ConfigServerClient();
                        csClient.Connect(configServer);

                        byte[] tempCdr = csClient.GetContentDescriptionRecord(cdrHash);

                        if (tempCdr == null)
                            continue;

                        if (tempCdr.Length == 0)
                            break;

                        using (MemoryStream ms = new MemoryStream(tempCdr))
                        using (BlobReader reader = BlobReader.CreateFrom(ms))
                            cdrObj = (CDR)BlobTypedReader.Deserialize(reader, typeof(CDR));

                        using (FileStream fs = File.Open(filename, FileMode.Create))
                        using (DeflateStream ds = new DeflateStream(fs, CompressionMode.Compress))
                            ProtoBuf.Serializer.Serialize<CDR>(ds, cdrObj);

                        using (SHA1Managed sha = new SHA1Managed())
                        {
                            config.CDRHash = sha.ComputeHash(tempCdr);
                        }
                        config.CDRCacheTime = DateTime.Now.AddMinutes(30);
                        CDRConfig.Save();
                        break;
                    }
                    catch (Exception ex)
                    {
                        typeof(CDRManager).Error(ex, string.Format("Warning: Unable to download CDR from config server {0}", configServer));
                    }
                }

                if (cdrObj != null)
                {
                    typeof(CDRManager).Info("Done");
                }
                else if (!File.Exists(filename))
                {
                    typeof(CDRManager).Error("Unable to download CDR");
                }
            }
            else
            {
                typeof(CDRManager).Info("Load cached copy");
                using (FileStream fs = File.Open(filename, FileMode.Open))
                using (DeflateStream ds = new DeflateStream(fs, CompressionMode.Decompress))
                    cdrObj = ProtoBuf.Serializer.Deserialize<CDR>(ds);

                typeof(CDRManager).Info("Done");
            }
        }

        public static void PrepareServers()
        {
            typeof(CDRManager).Info("Building Steam2 server cache...");

            CDRConfig.Settings config = CDRConfig.Instance;
            if (DateTime.Now > config.ServerCacheTime)
            {
                foreach (IPEndPoint gdServer in GeneralDSClient.GDServers)
                {
                    BuildServer(gdServer, config.ConfigServers, ESteam2ServerType.ConfigServer);
                    BuildServer(gdServer, config.CSDSServers, ESteam2ServerType.CSDS);
                }

                if (config.ConfigServers.Count > 0 && config.CSDSServers.Count > 0)
                {
                    config.ServerCacheTime = DateTime.Now.AddDays(30);
                    CDRConfig.Save();

                    typeof(CDRManager).Info("Done");
                    return;
                }
                else if (config.CSDSServers.Count == 0 || config.ConfigServers.Count == 0)
                {
                    typeof(CDRManager).Info("Unable to get server list");
                    return;
                }
            }
            typeof(CDRManager).Info("Done");
        }

        private static void BuildServer(IPEndPoint gdServer, HashSet<IPEndPoint> list, ESteam2ServerType type)
        {
            try
            {
                GeneralDSClient gdsClient = new GeneralDSClient();
                gdsClient.Connect(gdServer);

                IPEndPoint[] servers = gdsClient.GetServerList(type);
                IPEndPoint item;
                for (int i = 0; i < servers.Length; i++)
                {
                    item = servers[i];
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
                gdsClient.Disconnect();
            }
            catch (Exception ex)
            {
                typeof(CDRManager).Error(ex, string.Format("Warning: Unable to connect to GDS {0} to get list of {1} servers.", gdServer, type));
            }
        }
    }
}
