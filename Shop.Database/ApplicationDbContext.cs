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
    }
}
