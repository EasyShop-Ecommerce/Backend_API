using EasyShop.Core.Entities;

namespace EasyShop.API.DTOs
{
    public class OrderDetailsDTO
    {
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime Order { get; set; }
        public string ProductName { get; set; }
        public string SectionName { get; set; }


    }
}
