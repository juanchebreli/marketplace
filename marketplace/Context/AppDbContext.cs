using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using marketplace.Models;

namespace marketplace.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductOnSale> ProductsOnSale { get; set; }
		public DbSet<Purchase> Purchases { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
            });

			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(e => e.id);
			});

			modelBuilder.Entity<Permission>(entity =>
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
			modelBuilder.Entity<Purchase>(entity =>
			{
				entity.HasKey(e => e.id);
			});

			modelBuilder.Entity<PaymentMethod>(entity =>
			{
				entity.HasKey(e => e.id);
				entity.HasDiscriminator<string>("payment_type")
				.HasValue<CardMethod>("card_method")
				.HasValue<CashMethod>("cash_method");
			});
		}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}

