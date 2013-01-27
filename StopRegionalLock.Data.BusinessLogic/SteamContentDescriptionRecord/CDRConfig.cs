using Newtonsoft.Json;
using StopRegionalLock.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    public static class CDRConfig
    {
        private const string SECTION_NAME = "CDRConfig";

        #region IPEndpointConverter implementation

        private class IPEndpointConverter : JsonConverter
        {
            [DebuggerDisplay("IP={IP} Port={Port}")]
            public class IPEndPointStub
            {
                public string IP;
                public int Port;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteStartArray();
                foreach (IPEndPoint ipEndPoint in (HashSet<IPEndPoint>)value)
                {
                    IPEndPointStub item = new IPEndPointStub { IP = ipEndPoint.Address.ToString(), Port = ipEndPoint.Port };
                    serializer.Serialize(writer, item);
                }
                writer.WriteEndArray();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object value, JsonSerializer serializer)
            {
                HashSet<IPEndPoint> result = new HashSet<IPEndPoint>();
                IPEndPointStub[] array = serializer.Deserialize<IPEndPointStub[]>(reader);
                for (int i = 0; i < array.Length; i++)
                {
                    IPEndPointStub item = array[i];
                    result.Add(new IPEndPoint(IPAddress.Parse(item.IP), item.Port));
                }
                return result;
            }

            public override bool CanConvert(Type objectType)
            {
                return typeof(HashSet<IPEndPoint>).IsAssignableFrom(objectType);
            }
        } 

        #endregion

        public class Settings
        {
            public byte[] CDRHash { get; set; }
            public DateTime CDRCacheTime { get; set; }

            [JsonConverter(typeof(IPEndpointConverter))]
            public HashSet<IPEndPoint> ConfigServers = new HashSet<IPEndPoint>();

            [JsonConverter(typeof(IPEndpointConverter))]
            public HashSet<IPEndPoint> CSDSServers = new HashSet<IPEndPoint>();

            public DateTime ServerCacheTime;
        }

        public static Settings Instance
        {
            get
            {
                return RuntimeConfig.GetSection<Settings>(SECTION_NAME, () => new Settings());
            }
        }

        public static void Save()
        {
            RuntimeConfig.Save();
        }
    }
}
