using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Shipper
	{
		public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

		public decimal PricePerKm { get; set; }

		public ICollection<Order> Orders { get; set; }= new List<Order>();
	}

}
