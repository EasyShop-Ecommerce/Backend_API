using EasyShop.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class Customer
	{
		public int Id { get; set; }

		[Required]
		[RegularExpression("^[a-zA-Z]*$",ErrorMessage ="Invalid Name")]
		public string Name { get; set; }

		[RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Invalid Email")]
		[Required]
		public string Email { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string City { get; set; }

		public string Government { get; set; }
		[Required]
		[RegularExpression("^01[0125][0-9]{8}$",ErrorMessage ="Invalid Egyptian Number")]
		public string Phone { get; set; }

		public ICollection<CreditCard> CreditCards { get;  }= new List<CreditCard>();

		public  ICollection<Order> Orders { get;  }=new List<Order>();
		public ICollection<Review> Reviews { get; } = new List<Review>();
	}

}
