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

        public string Description { get; set; }

        public string Section { get; set; }

    }
}
