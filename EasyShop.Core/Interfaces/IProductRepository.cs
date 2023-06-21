﻿using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
	public interface IProductRepository
	{
		
		Task<IReadOnlyList<Product>> GetAllProducts();

		Task<Product> GetProductById(int id);	

		Task<int> AddProduct(Product product);

		Task<int> UpdateProduct(int id,Product product);

		Task<int> DeleteProduct(int id);
	}
}
