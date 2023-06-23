using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class PaymentMethod
	{
		public int Id { get; set; }

		[Required]
		public string Method { get; set; } = string.Empty;

		public ICollection<Order> Orders { get; set; }
	}
}
