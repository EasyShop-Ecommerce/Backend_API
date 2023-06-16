namespace EasyShop.Core.Entities
{
	public class Store
	{
		public int Id { get; set; }
		public string Location { get; set; }

		public ICollection<StoreProduct> StoreProducts { get; set; }
	}

}
