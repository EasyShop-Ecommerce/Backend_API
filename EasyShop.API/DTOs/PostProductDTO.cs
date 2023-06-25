namespace EasyShop.API.DTOs
{
    public class PostProductDTO:PostPutProductDTO
    {
        public List<IFormFile> ImagesOfProduct { get; set; }
    }
}
