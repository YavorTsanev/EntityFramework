using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PetStore.Common;
using PetStore.Models;

namespace PetStore.Data
{
    public class PetStoreDbContext : DbContext
    {
        public PetStoreDbContext()
        {
            
        }

        public PetStoreDbContext(DbContextOptions options) 
            :base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfigurations.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientProduct>().HasKey(x => new {x.ClientId, x.ProductId});
        }
    }
}
