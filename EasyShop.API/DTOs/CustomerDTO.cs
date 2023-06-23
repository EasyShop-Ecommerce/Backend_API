using EasyShop.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyShop.API.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Government { get; set; }
        public List<decimal> orders { get; set; } =new List<decimal>();
        public List<string> creditCards { get; set; }=new List<string>();

        //public ICollection<OrderDTO> orders { get; set; }
        //public ICollection<CreditCardDTO> creditCards { get; set; }

    }
}
