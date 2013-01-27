using BLToolkit.Data;
using MaxMind.GeoIP;
using StopRegionalLock.Common;
using StopRegionalLock.Common.GeoLocation;
using StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord;
using System;
using System.Configuration;
using System.Diagnostics;
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

            CDRConfig.Instance.ServerCacheTime = DateTime.Now;
            CDRConfig.Instance.ConfigServers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
            CDRConfig.Instance.ConfigServers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8088));
            CDRConfig.Instance.CSDSServers.Add(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9090));
            CDRConfig.Save();

            CDRConfig.Instance.ServerCacheTime.ToShortDateString();

            using (var db = 
                new DbManager(ConfigurationManager.AppSettings["DbProvider"], string.Empty))
            {
                db.SetCommand("SELECT 1");
                int i = db.ExecuteScalar<int>();
                Debug.Write(i);
            }

            Country c = GeoManager.Instance.GetCountryByIp("194.135.115.89");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}