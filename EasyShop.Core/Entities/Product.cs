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
}
