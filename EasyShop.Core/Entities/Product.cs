using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities 
{

	public class Product
	{
		public int Id { get; set; }

		public string BrandName { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage ="Please Give Title to the product")]
		public string Title { get; set; }

		public string Description { get; set; }

        public int AvailableQuantity { get; set; }

        [Required(ErrorMessage = "Please Provide Price to the product")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string Material { get; set; }

        public string OperatingSystem { get; set; }

        public string HardDiskSize { get; set; }

        public string SpecialFeatures { get; set; }

        public string MemoryStorageCapacity { get; set; }

        [ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		public virtual SubCategory SubCategory{ get; set; }

        [ForeignKey("Shipper")]
        public int? ShipperId { get; set; }
        public virtual Shipper Shipper { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }=new HashSet<ProductImage>();

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        //public virtual ICollection<ProductSeller> ProductSellers { get; set; } =new HashSet<ProductSeller>();

        // public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public double ReviewsAverage
        {
            get
            {
                if (Reviews != null && Reviews.Any())
                    return Reviews.Average(r => r.Rate);

                return 0; 
            }
        }

        public int ReviewsCount
        {
            get
            {
                return Reviews?.Count ?? 0;
            }
        }

    }
}
