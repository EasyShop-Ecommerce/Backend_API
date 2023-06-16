﻿namespace EasyShop.Core.Entities
{
	public class SubCategory
	{
		public int Id { get; set; }
		public string SubCategoryName { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public ICollection<Section> Sections { get; set; }
	}

}
