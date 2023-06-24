﻿using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository productRepo;
        //private readonly IGenericRepository<Product> productRepo;

        public ProductController(IProductRepository _productRepo)
		{
            productRepo = _productRepo;
        }

		[HttpGet]
		public async Task<ActionResult<List<ProductWithCategoryAndReviewsDTO>>> GetAllProducts()
		{
			IReadOnlyList<Product> products = await productRepo.GetAllProducts();
			return Ok(products.Select(p => new ProductWithCategoryAndReviewsDTO()
			{
				Id = p.Id,
				BrandName = p.BrandName,
				Description = p.Description,
				Title = p.Title,
				Price = p.Price,
                OperatingSystem = p.OperatingSystem ?? null,
                HardDiskSize = p.HardDiskSize ?? null,
                MemoryStorageCapacity = p.MemoryStorageCapacity ?? null,
                SpecialFeatures = p.SpecialFeatures ?? null,
                SubCategory = p.SubCategory != null ? p.SubCategory.SubCategoryName : null,
                Category = p.SubCategory != null ? p.SubCategory.Category?.CategoryName : null,
				ReviewsAverage=p.ReviewsAverage,
				ReviewsCount=p.ReviewsCount,
            }));
		}


		[HttpGet("{id:int}", Name = "GetOneProductRoute")]
		public async Task<ActionResult<ProductWithCategoryAndReviewsDTO>> GetProduct(int id)
		{
			//var spec = new GetProductWithReviews(id);
			Product productToReturn = await productRepo.GetProductById(id);
			if (productToReturn == null)
			{
				return NotFound();
			}
			ProductWithCategoryAndReviewsDTO product = new ProductWithCategoryAndReviewsDTO()
			{
				Id = productToReturn.Id,
				BrandName = productToReturn.BrandName,
				Description = productToReturn.Description,
				Title = productToReturn.Title,
				Price = productToReturn.Price,
                Material= productToReturn.Material??null,
                OperatingSystem= productToReturn.OperatingSystem??null,
                HardDiskSize= productToReturn.HardDiskSize ?? null,
                MemoryStorageCapacity=productToReturn.MemoryStorageCapacity??null,
                SpecialFeatures= productToReturn.SpecialFeatures??null,
                SubCategory = productToReturn.SubCategory != null ? productToReturn.SubCategory.SubCategoryName : null,
                Category = productToReturn.SubCategory != null ? productToReturn.SubCategory.Category?.CategoryName : null,
                ReviewsAverage = productToReturn.ReviewsAverage,
                ReviewsCount = productToReturn.ReviewsCount,
            };
			return Ok(product);
		}


		[HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (product == null) return BadRequest();
            if (ModelState.IsValid)
            {
                // Check if the specified SubCategoryId exists
                if (!await productRepo.SubCategoryExists(product.SubCategoryId))
                {
                    ModelState.AddModelError("SubCategoryId", "Invalid SubCategory ID");
                    return BadRequest(ModelState);
                }

                await productRepo.AddProduct(product);
                string url = Url.Link("GetOneProductRoute", new { id = product.Id });
                return Created(url, product);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (product == null) return BadRequest();
            if (id != product.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    int rowsAffected = await productRepo.UpdateProduct(id, product);

                    if (rowsAffected > 0)
                    {
                        return Ok("Product Updated Successfully");
                    }
                    else
                    {
                        return NotFound(); 
                    }
                }
                catch (DbUpdateException ex)
                {
                    return StatusCode(500, "An error occurred while updating the product.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				int rowsDeleted = await productRepo.DeleteProduct(id);
				return Ok($"{rowsDeleted} row(s) deleted successfully.");
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
				return StatusCode(500, "Internal server error");
			}
		}      
	}
}
