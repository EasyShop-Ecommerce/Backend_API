namespace EasyShop.Core.Entities
{
	public class PaymentMethod
	{
		public int Id { get; set; }

		public string Method { get; set; } = string.Empty;

		public virtual ICollection<Order> Orders { get; set; }
	}
}
