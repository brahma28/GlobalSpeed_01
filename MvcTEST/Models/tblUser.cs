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
    
    public partial class tblUser
    {
        public tblUser()
        {
            this.tblResults = new HashSet<tblResult>();
            this.tblSchedules = new HashSet<tblSchedule>();
            this.tblTerms = new HashSet<tblTerm>();
            this.tblTrainers = new HashSet<tblTrainer>();
            this.tblTrainers1 = new HashSet<tblTrainer>();
        }
    
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PIN { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> Weight { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<long> RFID { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> UserGroupID { get; set; }
        public Nullable<int> SportID { get; set; }
        public bool HasPicture { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsActiv { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> Role { get; set; }
        public byte[] Picture { get; set; }
    
        public virtual ICollection<tblResult> tblResults { get; set; }
        public virtual ICollection<tblSchedule> tblSchedules { get; set; }
        public virtual tblSport tblSport { get; set; }
        public virtual ICollection<tblTerm> tblTerms { get; set; }
        public virtual ICollection<tblTrainer> tblTrainers { get; set; }
        public virtual ICollection<tblTrainer> tblTrainers1 { get; set; }
        public virtual tblUserGroup tblUserGroup { get; set; }
    }
}
