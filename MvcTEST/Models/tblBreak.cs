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
    
    public partial class tblBreak
    {
        public int BreakID { get; set; }
        public string BreakName { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> DownOnTime { get; set; }
        public Nullable<int> DownOnCount { get; set; }
        public Nullable<int> Signal1Time { get; set; }
        public byte[] Signal1Acustic { get; set; }
        public Nullable<int> Signal2Time { get; set; }
        public byte[] Signal2Acustic { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
