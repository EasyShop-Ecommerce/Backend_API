using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class BagsProduct: Product
	{
		public int Id { get; set; }

		[Required]
		public string Material { get; set; }

		[Required]
		public string Type { get; set; }

		public double Width { get; set; }

		public double Height { get; set; }

		public double Length{ get; set; }

		public string Gender { get; set; }

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }
	}

}
