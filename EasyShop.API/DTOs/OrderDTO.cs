namespace EasyShop.API.DTOs
{
    public class OrderDTO
    {
        public decimal TotalPrice { get; set; }

        public decimal ShipPrice { get; set; }

        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string CustomerPhone { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string SellerName { get; set; }
        //public string ShipperName { get; set; }
        public List<string> Products { get; set; } = new List<string>(); 
        public List<int> Quantity { get; set; } = new List<int>();

    }
}
