namespace EasyShop.Core.Entities
{
	//may be SellerProduct better
	public class StoreProduct
	{
		//public int ProductId { get; set; }
		public int SellerId { get; set; }
		public int StoreId { get; set; }
		public int Quantity { get; set; }

		//public Product Product { get; set; }
		public Seller Seller { get; set; }
		public Store Store { get; set; }
	}

}
