using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
	public interface IProductRepository
	{
		//Methods Signature that deals with product

		Task<IReadOnlyList<Product>> GetAll();

		Task<Product> GetProductById(int id);	

		Task<int> AddProduct(Product product);
	}
}
