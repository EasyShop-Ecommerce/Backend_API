using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class SubCategory
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "SubCategory Name Is Required")]
        public string SubCategoryName { get; set; }

        [Required(ErrorMessage = "SubCategory Image Is Required")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$",
            ErrorMessage = "Invalid image format. Only JPG, JPEG, and PNG formats are allowed.")]
        public string SubCategoryImage { get; set; }

        [ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }

}
