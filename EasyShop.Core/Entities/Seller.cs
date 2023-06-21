namespace EasyShop.Core.Entities
{
	public class Seller
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string MiddleName { get; set; }
		public string LastName { get; set; } = string.Empty;
		public string SSN { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string BusinessName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Street { get; set; }
		public string City { get; set; } = string.Empty;
		public string Governorate { get; set; } = string.Empty;

		public virtual ICollection<Product> Products { get; set; }
		public virtual ICollection<StoreProduct> StoreProducts { get; set; }
		public virtual ICollection<Order> Orders { get;set; }
	}

}
