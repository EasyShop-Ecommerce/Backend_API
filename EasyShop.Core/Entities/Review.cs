using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class Review
	{
		[Range(1,5)]
		public int Rate { get; set; }

		public string Comment { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }		
	}

}
