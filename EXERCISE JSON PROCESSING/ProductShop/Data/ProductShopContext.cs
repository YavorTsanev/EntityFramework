using Microsoft.EntityFrameworkCore;
using ProductShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Data
{
    public class ProductShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public ProductShopContext()
        {

        }

        public ProductShopContext(DbContextOptions options) 
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=ProductShop;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>().HasKey( cp => new {cp.CategoryId, cp.ProductId });

            modelBuilder.Entity<Product>().HasOne(p => p.Buyer).WithMany(x => x.ProductsBought).HasForeignKey(p => p.BuyerId);

            modelBuilder.Entity<Product>().HasOne(p => p.Seller).WithMany(x => x.ProductsSold).HasForeignKey(p => p.SellerId);
        }
    }
}
