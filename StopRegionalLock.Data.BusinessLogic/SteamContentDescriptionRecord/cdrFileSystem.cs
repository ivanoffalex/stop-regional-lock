using ProtoBuf;
using SteamKit2.Blob;
using System.Collections.Generic;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    [ProtoContract]
    public class cdrFileSystem
    {
        [BlobField(1)]
        [ProtoMember(1)]
        public int ApplicationId;

        [BlobField(2)]
        [ProtoMember(2)]
        public string Name;

        [BlobField(4)]
        [ProtoMember(3)]
        public string Platform;
    }
}
