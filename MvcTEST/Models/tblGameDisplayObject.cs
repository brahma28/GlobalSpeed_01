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
    
    public partial class tblGameDisplayObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> IndexStart { get; set; }
        public Nullable<int> IndexEnd { get; set; }
        public Nullable<short> ValueType { get; set; }
        public string Value { get; set; }
        public Nullable<int> PositionX { get; set; }
        public Nullable<int> PositionY { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> GameID { get; set; }
    
        public virtual tblGame tblGame { get; set; }
    }
}