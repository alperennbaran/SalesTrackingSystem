﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesManagementSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SalesTrackingSystemEntities : DbContext
    {
        public SalesTrackingSystemEntities()
            : base("name=SalesTrackingSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admintable> admintable { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<city> city { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<department> department { get; set; }
        public virtual DbSet<expense> expense { get; set; }
        public virtual DbSet<faultdetail> faultdetail { get; set; }
        public virtual DbSet<invoicedetail> invoicedetail { get; set; }
        public virtual DbSet<invoiceinfo> invoiceinfo { get; set; }
        public virtual DbSet<mynotes> mynotes { get; set; }
        public virtual DbSet<product> product { get; set; }
        public virtual DbSet<productacceptance> productacceptance { get; set; }
        public virtual DbSet<productmovement> productmovement { get; set; }
        public virtual DbSet<producttracking> producttracking { get; set; }
        public virtual DbSet<staff> staff { get; set; }
    }
}