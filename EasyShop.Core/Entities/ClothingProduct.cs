namespace EasyShop.Core.Entities
{
	public class ClothingProduct : Product
	{
		public int Id { get; set; }		

		public string Material { get; set; }

		public string Type { get; set; }	//Try Using Enum
	}

}
