using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ClothingProduct: Product
	{
		public int Id { get; set; }		

		public string Material { get; set; }

		public string Gender { get; set; }  //Try Using Enum

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }

	}

}
