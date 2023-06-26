using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class PostPutProductDTO
    {
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Please Give Title to the product")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Provide Price to the product")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string OperatingSystem { get; set; }

        public string HardDiskSize { get; set; }

        public string SpecialFeatures { get; set; }

        public string MemoryStorageCapacity { get; set; }

        public int SubCategoryId { get; set; }

        public int ShipperId { get; set; }

    }
}
