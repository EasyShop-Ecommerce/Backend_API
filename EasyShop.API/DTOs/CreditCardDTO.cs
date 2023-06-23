using EasyShop.Core.Entities;

namespace EasyShop.API.DTOs
{
    public class CreditCardDTO
    {
        public string Cardholder_name { get; set; } = string.Empty;
        public string CardNumber { get; set; }
        public  DateTime ExpirationDate { get; set; }

        public string Customer { get; set; } 
    }
}
