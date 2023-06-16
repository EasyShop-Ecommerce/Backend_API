namespace EasyShop.Core.Entities
{
	public class Shipper
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal PricePerKm { get; set; }

		public ICollection<Order> Orders { get; set; }
	}

}
