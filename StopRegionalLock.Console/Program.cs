using BLToolkit.DataAccess;
using StopRegionalLock.Data;
using StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopRegionalLock.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string provider = ConfigurationManager.AppSettings["DbProvider"];

            using (var db =
                new StopRegionalLockContext(ConfigurationManager.AppSettings["DbProvider"], string.Empty))
            {
                var items = from c in db.Countries where c.Name.StartsWith("U") select c.Name;
                var iii = items.ToArray();
            }

            CDRSynchronizer();
        }

        private static void CDRSynchronizer()
        {
            CDRManager.PrepareServers();
            CDRManager.Update();
        }
    }
}
