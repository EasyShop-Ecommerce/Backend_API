using EasyShop.Core.Entities;

namespace EasyShop.API.DTOs
{
    public class PaymentMethodDTO
    {
        public string Method { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }=new List<OrderDTO>();

    }
}
