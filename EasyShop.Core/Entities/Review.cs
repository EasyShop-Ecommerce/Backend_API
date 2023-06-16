namespace EasyShop.Core.Entities
{
	public class Review
	{
		public int CustomerId { get; set; }
		//public int ProductId { get; set; }
		public int Rate { get; set; }
		public string Comment { get; set; }

		public Customer Customer { get; set; }
		//public Product Product { get; set; }
	}

}
