using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class MobileAccessoriesProduct : Product
	{
		public int Id { get; set; }
	
		public string CompatiblePhoneModels { get; set; }

		public string Material { get; set; }

		public string Pattern { get; set; }

		public string EmbellishmentFeature { get; set; }

		public string SpecialFeatures { get; set; }

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }

	}
}
