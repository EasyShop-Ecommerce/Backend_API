namespace EasyShop.API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; } 
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShipPrice { get; set; }
        public int Qty { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public int PaymentMethodId { get; set; }
        public int? SellerId { get; set; }

        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string Status { get; set; }    
        public string PaymentMethod { get; set; }       
        public string SellerBusinessName { get; set; }

        //public string ShipperName { get; set; }
        //public List<string> Products { get; set; } = new List<string>(); 
        //public List<int> Quantity { get; set; } = new List<int>();

    }
}
