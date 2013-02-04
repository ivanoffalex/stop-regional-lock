using StopRegionalLock.Data;
using StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

            var countries = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures)
                .Select(p => new 
                { 
                    Id = p.LCID,
                    Name = p.Name,
                    EnglishName = p.EnglishName,
                    NativeName = p.NativeName,
                    ShortName = p.TwoLetterISOLanguageName
                });

            foreach (var c in countries)
            {
                Debug.WriteLine(c);
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
