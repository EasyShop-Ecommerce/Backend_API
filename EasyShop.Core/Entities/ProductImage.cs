using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ProductImage
	{
		public int Id { get; set; }

        [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Invalid color format.")]
        public string Color { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$", ErrorMessage = "Invalid image format. Only JPG, JPEG, and PNG formats are allowed.")]
        public string ImageUrl { get; set; }

		[Required]
		public bool IsDefault { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
