using BLToolkit.Data;

namespace StopRegionalLock.Data
{
    public partial class StopRegionalLockContext : DbManager
    {
        public StopRegionalLockContext(string providerName, string configuration)
            : base(providerName, configuration)
        {
        }
    }
}
