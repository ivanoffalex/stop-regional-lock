using StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopRegionalLock.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CDRSynchronizer();
        }

        private static void CDRSynchronizer()
        {
            CDRManager.PrepareServers();
            CDRManager.Update();
        }
    }
}
