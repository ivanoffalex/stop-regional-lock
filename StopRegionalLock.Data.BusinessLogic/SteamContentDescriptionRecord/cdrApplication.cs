using ProtoBuf;
using SteamKit2.Blob;
using System.Collections.Generic;
using System.Diagnostics;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    [DebuggerDisplay("ApplicationId={ApplicationId} Name={Name}")]
    [ProtoContract]
    public class CDRApplication
    {
        [BlobField(1)]
        [ProtoMember(1)]
        public int ApplicationId;

        [BlobField(2)]
        [ProtoMember(2)]
        public string Name;

        [BlobField(11)]
        [ProtoMember(3)]
        public int CurrentVersion;

        [BlobField(10)]
        [ProtoMember(4)]
        public List<CDRAppVersion> Versions;

        [BlobField(12)]
        [ProtoMember(5)]
        public List<CDRFileSystem> FileSystems;

        [BlobField(14)]
        [ProtoMember(6)]
        public Dictionary<string, string> UserDefined;

        [BlobField(16)]
        [ProtoMember(7)]
        public int BetaVersion;
    }

}
