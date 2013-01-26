using StopRegionalLock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    public static class CDRConfig
    {
        private const string SECTION_NAME = "CDRConfig";

        public class CDRSettings
        {
            //public byte[] CDRHash { get; set; }
            //public DateTime CDRCacheTime { get; set; }

            //public ServerList ConfigServers { get; set; }
            //public ServerList CSDSServers { get; set; }

            public DateTime ServerCacheTime { get; set; }
        }

        public static CDRSettings Instance
        {
            get
            {
                return RuntimeConfig.GetSection<CDRSettings>(SECTION_NAME, () => new CDRSettings());
            }
        }

        public static void Save()
        {
            RuntimeConfig.Save();
        }
    }
}
