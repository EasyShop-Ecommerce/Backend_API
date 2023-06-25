namespace EasyShop.API.DTOs
{
    public class ProductSellersDTO
    {
        public int ProductId { get; set; }

        public int SellerId { get; set; }

        public decimal Price { get; set; }

        public int ProductQuantity { get; set; }
    }
}
