using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyShop.Core.Entities
{
	public class ServersProduct:Product
	{
		public int Id { get; set; }
				
		public string ItemDimensions { get; set; }

		public string ItemWeight { get; set; }

		public string CompatibleDevices { get; set; }

		public string MountingType { get; set; }

		public string RequiredAssembly { get; set; }

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }

	}
}
