using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
	public class ProductRepository : IProductRepository
	{
		private readonly StoreContext Context;

        public ProductRepository(StoreContext _Context)
        {
				Context=_Context;
        }

		public async Task<IReadOnlyList<Product>> GetAll()
		{			
			return await Context.Products.ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await Context.Products.SingleOrDefaultAsync(p => p.Id == id);
		}

		public async Task<int> AddProduct(Product product)
		{
			await Context.Products.AddAsync(product);
			int row=Context.SaveChanges();
			return row;
		}

	}
}
