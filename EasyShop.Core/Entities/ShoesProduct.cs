using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ShoesProduct :Product
	{
		public int Id { get; set; }
		[Required]

		public string OuterMaterial { get; set; }

		public string ClosureType { get; set; }

		public string HeelType { get; set; }

		public string Solematerial { get; set; }

		public string BootsHeight { get; set; }

		public string Gender { get; set;}

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }

	}
}
