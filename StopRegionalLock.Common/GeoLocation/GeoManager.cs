using MaxMind.GeoIP;
using System;
using System.Web.Hosting;

namespace StopRegionalLock.Common.GeoLocation
{

    /// <summary>
    /// Singleton implementation in order to make the GeoLocation use the buffering.
    /// </summary>
    public sealed class GeoManager
    {
        private static volatile GeoManager _instance;
        private static object _syncRoot = new Object();
        private static LookupService _lookupService = null;

        private GeoManager(string dbFilename)
        {
            _lookupService = new LookupService(dbFilename, LookupService.GEOIP_MEMORY_CACHE);
        }

        public static GeoManager Initialize(string dbFilename)
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new GeoManager(dbFilename);
                }
            }

            return _instance;
        }

        public static GeoManager Instance
        {
            get
            {
                return Initialize(HostingEnvironment.MapPath("~/App_Data/GeoIP.dat"));
            }
        }

        public Country GetCountryByIp(string ip, bool retry = true)
        {
            Country country = new Country(string.Empty, string.Empty);

            //get country of the ip address
            try
            {
                country = _lookupService.getCountry(ip);
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    _instance = null;
                    _instance = Instance;
                    GetCountryByIp(ip, false);
                }
                else
                {
                    typeof(GeoManager).Error(ex);
                }
            }

            return country;
        }

    }
}
