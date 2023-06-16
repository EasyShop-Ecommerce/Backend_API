using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class Review
	{
		
		public int Rate { get; set; }

		public string Comment { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }		
	}

}
