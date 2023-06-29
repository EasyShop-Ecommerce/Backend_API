using EasyShop.Core.Entities;
using EasyShop.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
	public class DBContext:IdentityDbContext<AppUser>
	{

		public DBContext(DbContextOptions<DBContext>  options) : base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Seller> Sellers { get; set; }

		public DbSet<Shipper> Shippers { get; set; }

		public DbSet<ProductImage> ProductImages { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<SubCategory> SubCategories { get; set; }

		//public DbSet<Section> Sections { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Status> Status { get; set; }

		public DbSet<PaymentMethod> PaymentMethods { get; set; }

		public DbSet<CreditCard> CreditCards { get; set; }

		public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        //public DbSet<OrderDetail> OrderDetails { get; set; }

        //public DbSet<Store> Stores { get; set; }

        //public DbSet<ProductSeller> ProductSellers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            
            modelBuilder.Entity<IdentityUserLogin<string>>()
				.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
				.HasKey(r => new { r.UserId, r.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
				.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

       //     modelBuilder.Entity<ProductImage>()
						 //.HasKey(p => new { p.Id, p.ProductId });

			modelBuilder.Entity<Review>()
						.HasKey(r => new { r.ProductId, r.CustomerId });

            //modelBuilder.Entity<OrderDetail>()
            //			.HasKey(o => new { o.OrderId, o.ProductId });

            //modelBuilder.Entity<ProductSeller>()
            //		    .HasKey(s => new { s.ProductId, s.SellerId});

            //modelBuilder.Entity<Order>()
            //            .HasOne(o => o.Seller)
            //            .WithMany()
            //            .HasForeignKey(o => o.SellerId)
            //            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                        .HasIndex(s => s.CategoryName)
                        .IsUnique();

            modelBuilder.Entity<SubCategory>()
                        .HasIndex(s => s.SubCategoryName)
                        .IsUnique();

        }
    }
}
