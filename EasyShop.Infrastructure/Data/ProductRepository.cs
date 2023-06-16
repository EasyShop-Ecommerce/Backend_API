using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
	public class ProductRepository : IProductRepository
	{
		public Task<IReadOnlyList<Product>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Product> GetProductById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
