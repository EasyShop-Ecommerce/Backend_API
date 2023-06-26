namespace EasyShop.API.DTOs
{
    public class SubcategoryWithProducts
    {
        public int SubCategoryId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public List<int> ProductsIds { get; set; } = new List<int>();

    }
}
