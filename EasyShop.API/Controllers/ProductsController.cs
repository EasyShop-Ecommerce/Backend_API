using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository ProductRepo;

		public ProductsController(IProductRepository _ProductRepo)
		{
			ProductRepo = _ProductRepo;
		}

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetAll()
		{
			IReadOnlyList<Product> products = await ProductRepo.GetAll();
			return Ok(products);
		}

		[HttpGet("{id:int}",Name ="GetOneProductRoute")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			Product productToReturn=await ProductRepo.GetProductById(id);
			if (productToReturn == null) 
			{ 
				return NotFound();
			}
			return Ok(productToReturn);
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct(Product product)
		{
			if(ModelState.IsValid) 
			{
				await ProductRepo.AddProduct(product);
				string url = Url.Link("GetOneProductRoute",new {id=product.Id});
				return Created(url,product);
			}
			return BadRequest(ModelState);
		}
    }
}
