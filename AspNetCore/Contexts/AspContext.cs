using AspNetCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Contexts
{
    public class AspContext:IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Desktop-psjtqan;database=Ap;integrated Security=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(i => i.ProductCategories).WithOne(i => i.Product).HasForeignKey(i => i.ProductId);
            modelBuilder.Entity<Category>().HasMany(i => i.ProductCategories).WithOne(i => i.Category).HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<ProductCategory>().HasIndex(i=> new { 
            i.CategoryId,i.ProductId
            }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
