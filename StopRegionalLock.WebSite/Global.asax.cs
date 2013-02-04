using MaxMind.GeoIP;
using StopRegionalLock.Common;
using StopRegionalLock.Common.GeoLocation;
using StopRegionalLock.Data;
using StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace StopRegionalLock.WebSite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            typeof(MvcApplication).Info("Application");

            //CDRManager.PrepareServers();
            //CDRManager.Update();

            using (var db =
                new StopRegionalLockContext(ConfigurationManager.AppSettings["DbProvider"], string.Empty))
            {
                var items = from subs in db.Subscriptions where subs.SubscriptionId != 5 select subs.Name;
                var iii = items.ToArray();
            }

            MaxMind.GeoIP.Country c = GeoManager.Instance.GetCountryByIp("194.135.115.89");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}