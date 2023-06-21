using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	//may be SellerProduct better
	public class StoreProduct
	{
		public int Quantity { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		[ForeignKey("Seller")]
		public int SellerId { get; set; }
		public virtual Seller Seller { get; set; }

		[ForeignKey("Store")]
		public int StoreId { get; set; }
		public virtual Store Store { get; set; }
	}

}
