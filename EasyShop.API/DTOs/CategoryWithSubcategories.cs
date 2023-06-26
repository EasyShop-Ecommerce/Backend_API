namespace EasyShop.API.DTOs
{
    public class CategoryWithSubcategories
    {
        
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public List<string> Subcategories { get; set; } = new List<string>();

    }
}
