using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class Section
	{
		public int Id { get; set; }

		[Required (ErrorMessage ="Section Name Is Required")]
		public string SectionName { get; set; }

		[Required(ErrorMessage ="Section Image Is Required")]
        public string SectionImage{ get; set; }

        [ForeignKey("SubCategory")]
		public int SubCategoryId { get; set; }
		public SubCategory SubCategory { get; set; }

		public ICollection<Product> Products { get; set; }=new HashSet<Product>();
	}

}
