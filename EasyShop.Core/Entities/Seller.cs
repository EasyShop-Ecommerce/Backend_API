using EasyShop.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Seller
	{
		public int Id { get; set; }
		[Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; } = string.Empty;
		[Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
        public string MiddleName { get; set; }
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Invalid Name")]
		[Required]
        public string LastName { get; set; } = string.Empty;
		[Required]
		[RegularExpression("^([1-9]{1})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})[0-9]{3}([0-9]{1})[0-9]{1}$")]
		public string SSN { get; set; } = string.Empty;
		[Required]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Invalid Egyptian Number")]
        public string Phone { get; set; } = string.Empty;
		[Required]
		public string BusinessName { get; set; } = string.Empty;
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Invalid Email")]
        [Required]
        public string Email { get; set; } = string.Empty;
		[Required]
		public string Street { get; set; }
		[Required]
		public string City { get; set; } = string.Empty;
		public string Governorate { get; set; } 

		public ICollection<Product> Products { get; set; }= new List<Product>();
		public ICollection<StoreProduct> StoreProducts { get; set; }= new List<StoreProduct>();
		public ICollection<Order> Orders { get; set; } = new List<Order>();
	}

}
