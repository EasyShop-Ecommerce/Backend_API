using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ProductSeller
    {
		public int Quantity { get; set; }

        [Required(ErrorMessage = "Please Provide Price to the product")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }


        [ForeignKey("Product")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }

		[ForeignKey("Seller")]
		public int SellerId { get; set; }
		public virtual Seller Seller { get; set; }

		//[ForeignKey("Store")]
		//public int StoreId { get; set; }
		//public virtual Store Store { get; set; }
	}

}
