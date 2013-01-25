using ProtoBuf;
using SteamKit2.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    [ProtoContract]
    public class cdrAppVersion
    {
        [BlobField(2)]
        [ProtoMember(1)]
        public uint VersionId;

        [BlobField(5)]
        [ProtoMember(2)]
        public string DepotEncryptionKey;

        [BlobField(6)]
        [ProtoMember(3)]
        public bool IsEncryptionKeyAvailable;
    }
}
