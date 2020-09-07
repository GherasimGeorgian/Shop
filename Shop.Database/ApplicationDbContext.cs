using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
namespace Shop.Database
{
    public class ApplicationDbContext:IdentityDbContext
    {
        //Install-Package Microsoft.EntityFrameworkCoreInstall-Package
        // Install-Package Microsoft.EntityFrameworkCore.Tools
        //Add-Migration Initial -Project Shop.Database --> in cazul in care dorim sa adaugam migratia in alt assemblyname
        //
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        //The entity type 'OrderProduct' requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.If you intended to use a keyless entity type call 'HasNoKey()'.
           modelBuilder.Entity<OrderProduct>()
                .HasKey(x => new { x.ProductId, x.OrderId });
        }
    }
}
