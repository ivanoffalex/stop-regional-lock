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
            new StopRegionalLockContext(provider, string.Empty))
            {
                var items = from subs in db.Subscriptions where subs.SubscriptionId != 5 select subs.Name;
                var iii = items.ToArray();
            }

            string[] lines = File.ReadAllLines(@"..\..\..\countries.csv");
            var codes = lines.Skip(1).Select(p => new { Id = p.Split(',').First(), Name = p.Split(',').Skip(1).First().Replace("\"", string.Empty) }).ToList();

            Dictionary<string, RegionInfo> countries = new Dictionary<string, RegionInfo>();
            foreach (var code in codes)
            {
                RegionInfo ri = null;
                try
                {
                    ri = new RegionInfo(code.Id);
                } catch {}
                countries[code.Id] = ri;
            }

            IList<Country> list = new List<Country>();
            for (int i = 0; i < codes.Count; i++)
            {
                var item = codes.ElementAt(i);
                RegionInfo ri;
                countries.TryGetValue(item.Id, out ri);

                Country c = new Country { 
                    CountryId = i,
                    Name = ri != null ? ri.EnglishName : item.Name,
                    NativeName = ri != null ? ri.EnglishName : null,
                    Code = item.Id
                };

                list.Add(c);
            }

            using (var db =
            new StopRegionalLockContext(provider, string.Empty))
            {
                SqlQuery<Country> q = new SqlQuery<Country>(db);
                q.Insert(list);
            }

            CDRSynchronizer();
        }

        private static void CDRSynchronizer()
        {
            //CDRManager.PrepareServers();
            //CDRManager.Update();
        }
    }
}
