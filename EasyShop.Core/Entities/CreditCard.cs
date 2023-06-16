using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class CreditCard
	{
		public int Id { get; set; }

		public string Cardholder_name { get; set; } = string.Empty;

		public DateTime ExpirationDate { get; set; }

		[ForeignKey("Customer")]
		[Required]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = default!;
	}

}
