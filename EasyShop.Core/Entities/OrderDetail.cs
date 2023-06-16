using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class OrderDetail
	{

		public decimal UnitPrice { get; set; }

		public int Quantity { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}

}
