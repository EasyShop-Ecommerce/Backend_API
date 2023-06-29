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
		private readonly DBContext Context;

        public ProductRepository(DBContext _Context)
        {
				Context=_Context;
        }

		public async Task<IReadOnlyList<Product>> GetAllProducts()
		{			
			return await Context.Products
				                .Include(p => p.SubCategory)
								.ThenInclude(p => p.Category)
								.Include(p=>p.Reviews)
                                //.Include(p => p.ProductSellers)
                                .Include(p => p.ProductImages)
                                .Include(p=>p.Shipper)
                                .ToListAsync();
		}

		public async Task<Product> GetProductById(int id)
		{
			return await Context.Products
								.Include(p=>p.SubCategory)
								.ThenInclude(p => p.Category)
								.Include(p => p.Reviews)
								//.Include(p=>p.ProductSellers)
								.Include(p=>p.ProductImages)
                                .Include(p => p.Shipper)
                                .SingleOrDefaultAsync(p => p.Id == id);
		}

        public async Task<bool> SubCategoryExists(int subCategoryId)
        {
            return await Context.SubCategories.AnyAsync(s => s.Id == subCategoryId);
        }

        public async Task<Product> AddProduct(Product product)
		{
			await Context.Products.AddAsync(product);
			await Context.SaveChangesAsync();
			return product;
		}
	
		//public async Task<int> UpdateProduct(int id, Product product)
		//{
		//	Product oldProduct = await Context.Products.SingleOrDefaultAsync(p => p.Id == id);
		//	if (oldProduct != null)
		//	{
		//		oldProduct.Title = product.Title;
		//		oldProduct.BrandName = product.BrandName;
		//		oldProduct.Description = product.Description;
		//		oldProduct.Price = product.Price;
		//		oldProduct.SubCategoryId = product.SubCategoryId;
		//		try
		//		{
		//			int rowsAffected = await Context.SaveChangesAsync();
		//			return rowsAffected;
		//		}
		//		catch (DbUpdateException ex)
		//		{
		//			throw;
		//		}
		//	}
		//	else
		//	{
		//		return -1; 
		//	}
		//}


        public async Task<int> UpdateProduct(int id, Product product)
        {           
            try
            {
                var entry = Context.Entry(product);
                entry.State = EntityState.Modified;
                int rowsAffected = await Context.SaveChangesAsync();
                return rowsAffected;
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteProduct(int id)
		{
			try
			{
				if (id <= 0)
				{
					throw new ArgumentException("Invalid product ID.");
				}

				Product productToDelete = await Context.Products.SingleOrDefaultAsync(p => p.Id == id);

				if (productToDelete == null)
				{
					throw new ArgumentException("Product not found.");
				}

				Context.Products.Remove(productToDelete);
				int row = await Context.SaveChangesAsync();

				Console.WriteLine($"Deleted {row} row(s) from the database.");

				return row;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
				throw;
			}
		}

        public async Task<ICollection<ProductImage>> AddRangeAsync(ICollection<ProductImage> images)
        {
            await Context.AddRangeAsync(images);
            await Context.SaveChangesAsync();
            return images;
        }

        public async Task<ProductImage> AddProductImage(ProductImage productImage)
        {
            await Context.ProductImages.AddAsync(productImage);
            await Context.SaveChangesAsync();
            return productImage;
        }

        public List<ProductImage> GetProductImages(int productId, string color)
        {
            var product = Context.Products
										.Include(p => p.ProductImages)
										.FirstOrDefault(p => p.Id == productId);
			
            if (product != null)
            {
                var images = product.ProductImages
												   .Where(pi => pi.Color == color)
												   .ToList();

                return images;
            }
            if (color == "all") return product.ProductImages.ToList();
            return null; 
        }
    }
}
