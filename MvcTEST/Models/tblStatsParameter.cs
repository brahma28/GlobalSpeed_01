//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcTEST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblStatsParameter
    {
        public int StatsParameterID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<bool> PresentationSpider { get; set; }
        public Nullable<bool> PresentationLines { get; set; }
        public Nullable<bool> PresentationTable { get; set; }
        public Nullable<bool> PresentationBeam { get; set; }
        public Nullable<bool> ParameterTurntime { get; set; }
        public Nullable<bool> ParameterFlighttime { get; set; }
        public Nullable<bool> ParameterGroundcontacttime { get; set; }
        public Nullable<bool> ParameterLabel { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public Nullable<int> CompareUserID { get; set; }
    }
}
