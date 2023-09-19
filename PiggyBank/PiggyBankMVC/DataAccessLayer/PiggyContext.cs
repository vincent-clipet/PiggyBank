using Microsoft.EntityFrameworkCore;
using SchemaBuilder.models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiggyBankMVC.DataAccessLayer
{
    public class PiggyContext : DbContext
    {
        public PiggyContext(DbContextOptions<PiggyContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> order_details { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Manufacturer> manufacturers { get; set; }
        public DbSet<OrderStatus> order_statuses { get; set; }
        public DbSet<ReviewStatus> review_statuses { get; set; }

    }
}
