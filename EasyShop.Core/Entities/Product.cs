using System;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities 
{

	public class Product
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string BrandName { get; set; }

		public string Title { get; set; }

		public decimal Price { get; set; }

		public string Description { get; set; }

		public int SectionId { get; set; }
		public Section Section { get; set; }	
	}

	//public abstract class Product
	//{
	//	public int Code { get; set; }

	//	public string BrandName { get; set; }

	//	public string Color { get; set; }

	//	public string Size { get; set; }

	//	public string Price { get; set; }

	//	public string Description { get; set; }
	//}

}
