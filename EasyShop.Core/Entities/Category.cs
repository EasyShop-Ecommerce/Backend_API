using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		public string CategoryName { get; set; } = string.Empty;

		public ICollection<SubCategory> SubCategories { get; set; }

	}
}
