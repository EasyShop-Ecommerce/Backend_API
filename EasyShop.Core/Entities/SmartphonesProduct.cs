using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class SmartphonesProduct : Product
	{
		public int Id { get; set; }

		[Required]
		public string ModelName { get; set; }

		public string WirelessProvider { get; set; }
		[Required]
		public string MemoryStorageCapacity { get; set; }

		public string WirelessNetworkTechnology { get; set; }
		[Required]
		public string RAMMemoryInstalled { get; set; }

		public string ConnectivityTechnology { get; set;}
	}
}
