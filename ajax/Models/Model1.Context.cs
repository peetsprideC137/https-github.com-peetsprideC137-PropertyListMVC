﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ajax.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SampleDBContext1 : DbContext
    {
        public SampleDBContext1()
            : base("name=SampleDBContext1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<BidApproved> BidApproveds { get; set; }
        public virtual DbSet<BiddingProperty> BiddingProperties { get; set; }
        public virtual DbSet<BidRejected> BidRejecteds { get; set; }
        public virtual DbSet<Broker> Brokers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<OwnerAddOn> OwnerAddOns { get; set; }
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<PropertyDetail> PropertyDetails { get; set; }
        public virtual DbSet<PropertyQuestion> PropertyQuestions { get; set; }
        public virtual DbSet<Recfrombrok> Recfrombroks { get; set; }
        public virtual DbSet<tblStudent> tblStudents { get; set; }
        public virtual DbSet<dpt> dpts { get; set; }
    }
}