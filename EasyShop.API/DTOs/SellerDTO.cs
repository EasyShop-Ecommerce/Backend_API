using EasyShop.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class SellerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Street { get; set; }
        public string City { get; set; } = string.Empty;
        public string Governorate { get; set; }
        //public List<string> Products { get; set; }= new List<string>();
        //public List<int> StoreProducts { get; set; }=new List<int>();
        public List<ProductSellersDTO> SellerProducts { get; set; }=new List<ProductSellersDTO>();
        public List<DateTime> OrdersDate { get; set; } = new List<DateTime>();
        public List<decimal> OrdersPrice { get; set; } = new List<decimal>();

    }
}
