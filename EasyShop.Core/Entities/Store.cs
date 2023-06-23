using System.ComponentModel.DataAnnotations;

namespace EasyShop.Core.Entities
{
	public class Store
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="Store Location Is Required")] 
		public string Location { get; set; }

		public virtual ICollection<StoreProduct> StoreProducts { get; set; } = new List<StoreProduct>();
	}
}
