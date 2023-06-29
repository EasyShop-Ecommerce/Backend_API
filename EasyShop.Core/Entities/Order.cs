using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class Order
	{
		public int Id { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

		public decimal ShipPrice { get; set; }

		public DateTime Date { get; set; }

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }

		[ForeignKey("Status")]
		public int StatusId { get; set; }
		public virtual Status Status { get; set; }

		[ForeignKey("PaymentMethod")]
		public int PaymentMethodId { get; set; }
		public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Seller")]
        public int? SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        //[ForeignKey("Shipper")]
        //public int ShipperId { get; set; }
        //public virtual Shipper Shipper { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }= new List<OrderDetail>();
    }

}
