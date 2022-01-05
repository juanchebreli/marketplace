using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using marketplace.Models;
using marketplace.Interfaces;

namespace marketplace.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }

		public DbSet<ProductOnSale> ProductsOnSale { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
            });

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.id);
			});

			modelBuilder.Entity<ProductOnSale>(entity =>
			{
				entity.HasKey(e => e.id);
			});
		}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}

