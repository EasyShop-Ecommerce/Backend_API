using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		//private readonly IProductRepository ProductRepo;
        private readonly IGenericRepository<Product> productRepo;

        public ProductsController(IGenericRepository<Product> _productRepo)
		{
            productRepo = _productRepo;
        }

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetAllProducts()
		{
			IReadOnlyList<Product> products = await productRepo.GetAllAsync();
			return Ok(products);
		}

		[HttpGet("{id:int}",Name ="GetOneProductRoute")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new GetProductWithReviews(id);
			Product productToReturn = await productRepo.GetEntityWithSpec(spec);
			if (productToReturn == null) 
			{ 
				return NotFound();
			}
			return Ok(productToReturn);
		}

		//[HttpPost]
		//public async Task<ActionResult> AddProduct(Product product)
		//{
		//	if(ModelState.IsValid) 
		//	{
		//		await ProductRepo.AddProduct(product);
		//		string url = Url.Link("GetOneProductRoute",new {id=product.Id});
		//		return Created(url,product);
		//	}
		//	return BadRequest(ModelState);
		//}

		//[HttpPut("{id:int}")]
		//public async Task<ActionResult> UpdateProduct(int id,Product product)
		//{
		//	if(ModelState.IsValid)
		//	{
		//		int result=await ProductRepo.UpdateProduct(id,product);
		//		if (result >0) 
		//		{
		//			return StatusCode(204, "Product Updated Successfully");
		//		}
		//		else if(result ==-1) 
		//		{
		//			return NotFound("Product Not Found");
		//		}
		//		else
		//		{
		//			return StatusCode(500);
		//		}

		//	}
		//	else
		//	{
		//		return BadRequest(ModelState);
		//	}
		//}

		//[HttpDelete("{id:int}")]
		//public async Task<IActionResult> Delete(int id)
		//{
		//	try
		//	{
		//		int rowsDeleted = await ProductRepo.DeleteProduct(id);
		//		return Ok($"{rowsDeleted} row(s) deleted successfully.");
		//	}
		//	catch (ArgumentException ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
		//		return StatusCode(500, "Internal server error");
		//	}
		//}
	}
}
