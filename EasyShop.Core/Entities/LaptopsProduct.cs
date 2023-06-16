using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace EasyShop.Core.Entities
{
	public class LaptopsProduct :Product
	{
        public int Id { get; set; }
		[Required]
       
        public string HardDiskSize { get; set; }
		public string RAMMemoryInstalled { get; set;}
		public string OperatingSystem { get; set; }
		public string GraphicsDescription { get; set; }
		public string GraphicsCoprocessor { get; set; }
	}
}
