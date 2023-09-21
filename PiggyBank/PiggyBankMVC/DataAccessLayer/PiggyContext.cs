using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PiggyBankMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PiggyBankMVC.DataAccessLayer
{
    public class PiggyContext : IdentityDbContext<ApplicationUser>
    {
        public PiggyContext(DbContextOptions<PiggyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<IdentityUser>(entity => {entity.ToTable(name: "User");});
            modelBuilder.Entity<IdentityRole>(entity =>{entity.ToTable(name: "Role");});
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>{entity.ToTable("UserRoles");});
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>{entity.ToTable("UserClaims");});
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>{entity.ToTable("UserLogins");});
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>{entity.ToTable("RoleClaims");});
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>{entity.ToTable("UserTokens");});





            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<OrderStatus> OrderStatuses { get; set; }
        //public DbSet<ReviewStatus> ReviewStatuses { get; set; }

    }
}
