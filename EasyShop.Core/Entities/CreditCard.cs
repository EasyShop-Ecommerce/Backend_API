using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class CreditCard
	{
		public int Id { get; set; }

		[Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string Cardholder_name { get; set; } = string.Empty;

		[Required]
		public DateTime ExpirationDate { get; set; }

		[Required]
		[RegularExpression("^4[0-9]{12}(?:[0-9]{3})?$",ErrorMessage ="Invalid Card Number")]
		public string CardNumber { get; set; }

		[ForeignKey("Customer")]
		[Required]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = default!;
	}

}
