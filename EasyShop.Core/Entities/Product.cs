using System;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities 
{

	public class Product
	{
		[Key]
		public string Code { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }

		//public int SectionId { get; set; }
		//public Section Section { get; set; }

		//public ICollection<ProductImage> ProductImages { get; set; }
		//public ICollection<Review> Reviews { get; set; }
		//public ICollection<OrderDetail> OrderDetails { get; set; }
	}

	public interface IProduct
	{
		public string BrandName { get; set; }
		public string Color { get; set; }
		public string Size { get; set; }
		public string Price { get; set; }
		public string Description{ get; set; }
	}

	public  abstract class Product
	{
		public string BrandName { get; set; }
		public string Color { get; set; }
		public string Size { get; set; }
		public string Price { get; set; }
		public string Description { get; set; }
	}

}
