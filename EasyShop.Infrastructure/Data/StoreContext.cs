using EasyShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
	public class StoreContext:DbContext
	{

		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }

		//public DbSet<Seller> Sellers { get; set;}

		//public DbSet<Shipper> Shippers { get; set; }


		//public DbSet<ProductImage> ProductImages { get; set; }

		//public DbSet<Category> Categories { get; set; }

		//public DbSet<SubCategory> SubCategories { get; set; }

		//public DbSet<Section> Sections { get; set; }

		//public DbSet<LaptopsProduct> LaptopsProducts { get; set; }

		//public DbSet<Status> Status { get; set; }	

		//public DbSet<PaymentMethod> PaymentMethods { get; set; }

		//public DbSet<CreditCard> CreditCards { get; set;}

		//public DbSet<Order> Orders { get; set; }

		//public DbSet<OrderDetail> OrderDetails { get; set; }	

		//public DbSet<Review> Reviews { get; set; }

		//public DbSet<Store> Stores { get; set; }

		//public DbSet<StoreProduct> StoreProducts { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	//modelBuilder.Entity<ProductImage>()
		//	//			 .HasKey(p => new { p.Id, p.ProductId });

		//	//modelBuilder.Entity<Review>()
		//	//			.HasKey(r => new { r.ProductId, r.CustomerId });

		//	//modelBuilder.Entity<OrderDetail>()
		//	//		.HasKey(o => new { o.OrderId, o.ProductId });

		//	//modelBuilder.Entity<StoreProduct>()
		//	//		.HasKey(s => new { s.ProductId, s.SellerId,s.StoreId});

		//}
	}
}
