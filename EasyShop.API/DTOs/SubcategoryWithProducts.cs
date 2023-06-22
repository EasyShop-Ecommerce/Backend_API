namespace EasyShop.API.DTOs
{
    public class SubcategoryWithProducts
    {
        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public List<string> Products { get; set; } = new List<string>();

    }
}
