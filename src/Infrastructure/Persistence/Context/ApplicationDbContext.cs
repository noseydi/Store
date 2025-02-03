using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> products => Set<Product>();
        public DbSet<ProductBrand> productbrand => Set<ProductBrand>();
        public DbSet<ProductType> producttype => Set<ProductType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<ProductType>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<ProductBrand>().HasQueryFilter(x => x.IsDelete == false);

        }
    }
}
