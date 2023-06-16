namespace EasyShop.Core.Entities
{
	public class Section
	{
		public int Id { get; set; }
		public string SectionName { get; set; }

		public int SubCategoryId { get; set; }
		public SubCategory SubCategory { get; set; }

		//public ICollection<Product> Products { get; set; }
	}

}
