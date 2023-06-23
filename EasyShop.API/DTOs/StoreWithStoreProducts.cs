namespace EasyShop.API.DTOs
{
    public class StoreWithStoreProducts
    {
        public int  StoreId { get; set; }

        public string StoreLocation { get; set; }

        public List<StoreProductDTO> StoreProducts { get; set; } = new List<StoreProductDTO>();
    }

}
