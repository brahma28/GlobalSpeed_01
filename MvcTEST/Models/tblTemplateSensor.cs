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
    
    public partial class tblTemplateSensor
    {
        public int ID { get; set; }
        public Nullable<short> Number { get; set; }
        public Nullable<int> PositionX { get; set; }
        public Nullable<int> PositionY { get; set; }
        public bool IsBigSensor { get; set; }
        public Nullable<int> TemplateID { get; set; }
        public Nullable<int> DimensionX { get; set; }
    
        public virtual tblTemplate tblTemplate { get; set; }
    }
}
