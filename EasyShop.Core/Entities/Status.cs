namespace EasyShop.Core.Entities
{
	public class Status
	{
		public int Id { get; set; }

		public string StatusName { get; set; } = string.Empty;

		public ICollection<Order> Orders { get; set; }
	}
}
