namespace EasyShop.Core.Entities
{
	public class CreditCard
	{
		public int Id { get; set; }
		public string Cardholder_name { get; set; } = string.Empty;
		public DateTime ExpirationDate { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = default!;
	}

}
