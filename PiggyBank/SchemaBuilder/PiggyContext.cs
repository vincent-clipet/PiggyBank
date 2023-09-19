﻿using SchemaBuilder.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaBuilder
{
    public class PiggyContext : DbContext
    {
        public PiggyContext() : base("Server=DESKTOP-ISFDFVM;Database=piggybank;User Id=piggybank;Password=piggybank;")
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> order_details { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Address> adresses { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Manufacturer> manufacturers { get; set; }
        public DbSet<OrderStatus> order_statuses { get; set; }
        public DbSet<ReviewStatus> review_statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}