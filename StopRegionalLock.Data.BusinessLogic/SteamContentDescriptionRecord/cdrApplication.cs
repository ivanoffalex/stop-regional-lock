using ProtoBuf;
using SteamKit2.Blob;
using System.Collections.Generic;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    [ProtoContract]
    public class cdrApplication
    {
        [BlobField(2)]
        [ProtoMember(1)]
        public string Name;

        [BlobField(1)]
        [ProtoMember(2)]
        public int ApplicationId;

        [BlobField(11)]
        [ProtoMember(3)]
        public int CurrentVersion;

        [BlobField(10)]
        [ProtoMember(4)]
        public List<cdrAppVersion> Versions;

        [BlobField(12)]
        [ProtoMember(5)]
        public List<cdrFileSystem> FileSystems;

        [BlobField(14)]
        [ProtoMember(6)]
        public Dictionary<string, string> UserDefined;

        [BlobField(16)]
        [ProtoMember(7)]
        public int BetaVersion;
    }

}
