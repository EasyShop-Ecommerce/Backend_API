using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class ServersProduct : Product
	{
		public int Id { get; set; }
				
		public string ItemDimensions { get; set; }

		public string ItemWeight { get; set; }

		public string CompatibleDevices { get; set; }

		public string MountingType { get; set; }

		public string RequiredAssembly { get; set; }
	}
}
