using EasyShop.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Status
	{
		public int Id { get; set; }

		[Required]
		public string StatusName { get; set; }

		public ICollection<Order> Orders { get; set; } = new List<Order>();
	}
}
