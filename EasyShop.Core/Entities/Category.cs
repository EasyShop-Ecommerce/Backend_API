using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Category
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Category Name Is Required")]
        public string CategoryName { get; set; } = string.Empty;

        //public IFormFile CategoryImageFile { get; set; }

        [Required(ErrorMessage = "Category Image Is Required")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.png)$",
            ErrorMessage = "Invalid image format. Only JPG, JPEG, and PNG formats are allowed.")]
        public string CategoryImage { get; set; } = string.Empty;

        public virtual ICollection<SubCategory> SubCategories { get; set; }=new HashSet<SubCategory>();
	}
}
