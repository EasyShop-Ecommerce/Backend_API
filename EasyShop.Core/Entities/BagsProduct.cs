using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class BagsProduct : Product
	{
		public int Id { get; set; }

		[Required]
		public string Material { get; set; }

		[Required]
		public string Type { get; set; }

		public double Width { get; set; }

		public double Height { get; set; }

		public double Length{ get; set; }

		public string Gender { get; set; }
	}

}
