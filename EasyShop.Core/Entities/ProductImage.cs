using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ProductImage
	{
		public int Id { get; set; }

		public string Color { get; set; }

		public string ImageUrl { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}

}
