using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class OrderDetail
	{
		[Required]
		public decimal UnitPrice { get; set; }
		[Required]
		public int Quantity { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
	}

}
