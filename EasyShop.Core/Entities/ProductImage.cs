using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ProductImage
	{
        public int Id { get; set; }

		[Required]
        public string Color { get; set; }

        public byte[] Image { get; set; }
		
		public bool? IsDefault { get; set; }

		//public string ImagePath { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
