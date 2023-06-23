using EasyShop.Core.Entities;

namespace EasyShop.API.DTOs
{
    public class StatusDTO
    {
        public string StatusName { get; set; }

        public List<DateTime> orders { get; set; }=new List<DateTime>();
    }
}
