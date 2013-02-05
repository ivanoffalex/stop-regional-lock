﻿ 
 

 
 
 
 
//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by BLToolkit template for T4.
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------
using System;

using BLToolkit.Data;
using BLToolkit.Data.Linq;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace StopRegionalLock.Data
{
	public partial class StopRegionalLockContext : DbManager
	{
		public Table<Country>      Countries     { get { return this.GetTable<Country>();      } }
		public Table<Subscription> Subscriptions { get { return this.GetTable<Subscription>(); } }
	}

	[TableName(Owner="public", Name="\"Country\"")]
	public partial class Country
	{
		[MapField("\"CountryId\"") , PrimaryKey(1)] public Int32  CountryId  { get; set; } // integer
		[MapField("\"Code\"")                     ] public String Code       { get; set; } // character varying(2)(2)
		[MapField("\"Name\"")                     ] public String Name       { get; set; } // text
		[MapField("\"NativeName\"")               ] public String NativeName { get; set; } // text
		[MapField("\"System\"")                   ] public Int16  System     { get; set; } // smallint
	}

	[TableName(Owner="public", Name="\"Subscription\"")]
	public partial class Subscription
	{
		[MapField("\"SubscriptionId\""), PrimaryKey(1)] public Int32  SubscriptionId { get; set; } // integer
		[MapField("\"Name\"")                         ] public String Name           { get; set; } // text
		[MapField("\"BillingType\"")                  ] public Int16  BillingType    { get; set; } // smallint
	}
}
