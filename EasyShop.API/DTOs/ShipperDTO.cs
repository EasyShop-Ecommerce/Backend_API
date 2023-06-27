using EasyShop.Core.Entities;

namespace EasyShop.API.DTOs
{
    public class ShipperDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PricePerKm { get; set; }

        public List<int> ShipperProducts { get; set; }=new List<int>();

        //public List<DateTime> orders { get; set; }=new List<DateTime>();

        //public List<decimal> ordersPrice { get; set; } = new List<decimal>();

        //public List<decimal> ordersShip { get; set; } = new List<decimal>();




    }
}

