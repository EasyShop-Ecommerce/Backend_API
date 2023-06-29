using EasyShop.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string BrandName { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string OperatingSystem { get; set; }

        public string HardDiskSize { get; set; }

        public string SpecialFeatures { get; set; }

        public string MemoryStorageCapacity { get; set; }

        public string SubCategory { get; set; }

        public int SubCategoryId { get; set; }

        public string Category { get; set; }

        public int? CategoryId{ get; set; }

        public int ReviewsCount { get; set; }

        public double ReviewsAverage { get; set; }

        //public List<ProductSellersDTO> Sellers { get; set; }=new List<ProductSellersDTO>();

        public int? ShipperId { get; set; }

        public int SellerId { get; set; }

        public string DefaultImage { get;set; }
    }
}
