namespace EasyShop.Core.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public decimal TotalPrice { get; set; }
		public decimal ShipPrice { get; set; }

		public DateTime Date { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		public int StatusId { get; set; }
		public Status Status { get; set; }

		public int PaymentMethodId { get; set; }
		public PaymentMethod PaymentMethod { get; set; }

		public int ShipperId { get; set; }
		public Shipper Shipper { get; set; }

		public int SellerId { get; set; }
		public Seller Seller { get; set; }

		public ICollection<OrderDetail> OrderDetails { get; set; }
	}

}
