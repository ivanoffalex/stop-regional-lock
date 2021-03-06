﻿using ProtoBuf;
using SteamKit2.Blob;
using System.Collections.Generic;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    /// <summary>
    /// Content Description Record blob entry
    /// </summary>
    [ProtoContract]
    public class CDR
    {
        /// <summary>
        /// List of Applications
        /// </summary>
        [BlobField(1)]
        [ProtoMember(1)]
        public List<CDRApplication> Applications;

        /// <summary>
        /// List of Subscriptions
        /// </summary>
        [BlobField(2)]
        [ProtoMember(2)]
        public List<CDRSubscription> Subscriptions;

    }
}
