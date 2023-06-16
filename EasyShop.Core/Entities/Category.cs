namespace EasyShop.Core.Entities
{
	public class Category
	{
		public int Id { get; set; }
		public string CategoryName { get; set; } = string.Empty;

		public ICollection<SubCategory> SubCategories { get; set; }
	}

}
