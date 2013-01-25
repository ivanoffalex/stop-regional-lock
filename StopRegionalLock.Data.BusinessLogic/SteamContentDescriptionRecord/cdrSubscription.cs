using ProtoBuf;
using SteamKit2;
using SteamKit2.Blob;
using System.Collections.Generic;

namespace StopRegionalLock.Data.BusinessLogic.SteamContentDescriptionRecord
{
    /// <summary>
    /// Subscription blob entry
    /// </summary>
    [ProtoContract]
    public class cdrSubscription
    {
        /// <summary>
        /// Subscription Id
        /// </summary>
        [BlobField(1)]
        [ProtoMember(1)]
        public int SubscriptionId;

        /// <summary>
        /// Subscription name
        /// </summary>
        [BlobField(2)]
        [ProtoMember(2)]
        public int Name;

        /// <summary>
        /// Billing Type
        /// </summary>
        [BlobField(3)]
        [ProtoMember(3)]
        public EBillingType BillingType;

        /// <summary>
        /// Cost (in cents)
        /// </summary>
        [BlobField(4)]
        [ProtoMember(4)]
        public int Cost;

        ///// <summary>
        ///// Period (in minutes)
        ///// </summary>
        //[BlobField(5)]
        //[ProtoMember(5)]
        //public int Period;
        

        [BlobField(6)]
        [ProtoMember(5)]
        public List<int> ApplicationIds;

        /// <summary>
        /// On subscribe, run ApplicationId
        /// </summary>
        [BlobField(7)]
        [ProtoMember(6)]
        public int RunApplicationId;

        /// <summary>
        /// On subscribe, use launch option
        /// </summary>
        [BlobField(8)]
        [ProtoMember(7)]
        public int UseLaunchOption;

        /// <summary>
        /// Discounts
        /// </summary>
        [BlobField(10)]
        [ProtoMember(8)]
        public int Discounts;

        /// <summary>
        /// Is preorder
        /// </summary>
        [BlobField(11)]
        [ProtoMember(9)]
        public bool IsPreorder;

        /// <summary>
        /// Requires shipping address
        /// </summary>
        [BlobField(12)]
        [ProtoMember(10)]
        public bool RequiresShippingAddress;

        /// <summary>
        /// Domestic cost (in cents)
        /// </summary>
        [BlobField(13)]
        [ProtoMember(11)]
        public int DomesticCost;

        /// <summary>
        /// International cost (in cents)
        /// </summary>
        [BlobField(14)]
        [ProtoMember(12)]
        public int InternationalCost;

        /// <summary>
        /// Required key type
        /// </summary>
        [BlobField(15)]
        [ProtoMember(13)]
        public int RequiredKeyType;

        /// <summary>
        /// Is cyber cafe
        /// </summary>
        [BlobField(16)]
        [ProtoMember(14)]
        public bool IsCyberCafe;

        /// <summary>
        /// Game code
        /// </summary>
        [BlobField(17)]
        [ProtoMember(15)]
        public int GameCode;

        /// <summary>
        /// Game code description
        /// </summary>
        [BlobField(18)]
        [ProtoMember(16)]
        public int GameCodeDescription;

        /// <summary>
        /// Is disabled
        /// </summary>
        [BlobField(19)]
        [ProtoMember(17)]
        public bool IsDisabled;

        /// <summary>
        /// Requires cd
        /// </summary>
        [BlobField(20)]
        [ProtoMember(18)]
        public bool RequiresCD;

        /// <summary>
        /// TerritoryCode
        /// </summary>
        [BlobField(21)]
        [ProtoMember(19)]
        public int TerritoryCode;

        /// <summary>
        /// Is Steam3 subscription
        /// </summary>
        [BlobField(22)]
        [ProtoMember(20)]
        public bool IsSteam3;

        /// <summary>
        /// Extended Info
        /// </summary>
        [BlobField(23)]
        [ProtoMember(21)]
        public Dictionary<string, string> ExtendedInfo;       
    }
}
