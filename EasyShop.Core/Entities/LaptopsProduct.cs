using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		[ForeignKey("Section")]
		public int SectionId { get; set; }
		public Section Section { get; set; }

		public ICollection<Review> Reviews { get; set; }

	}
}
