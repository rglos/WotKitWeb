﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Domain.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WotKitEntities : DbContext
    {
        public WotKitEntities()
            : base("name=WotKitEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Battle> Battles { get; set; }
        public virtual DbSet<PlayerBattle> PlayerBattles { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Tank> Tanks { get; set; }
    }
}
