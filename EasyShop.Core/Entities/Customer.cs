﻿namespace EasyShop.Core.Entities
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string? Street { get; set; }
		public string City { get; set; }
		public string? Government { get; set; }
		//public string Phone { get; set; }

		//public ICollection<CreditCard> CreditCards { get; set; }
		//public ICollection<Order> Orders { get; set; }
		//public ICollection<Review> Reviews { get; set; }
	}

}
