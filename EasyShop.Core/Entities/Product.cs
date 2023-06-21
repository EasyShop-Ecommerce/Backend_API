using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities 
{

	public class Product
	{
		public int Id { get; set; }

		public string Code { get; set; }

		public string BrandName { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public decimal Price { get; set; }

		public string Description { get; set; }

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public virtual Section Section { get; set; }	

		public ICollection<Review> Reviews { get; set; }
	}
}
