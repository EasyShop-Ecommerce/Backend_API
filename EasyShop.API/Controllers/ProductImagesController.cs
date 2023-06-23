using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IGenericRepository<ProductImage> productImageRepo;

        public ProductImagesController(IGenericRepository<ProductImage> _productImageRepo)
        {
            productImageRepo = _productImageRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductImage>>> GetAll()
        {
            return Ok(await productImageRepo.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductImage>> GetById(int id)
        {
            var productImage = await productImageRepo.GetByIdAsync(id);
            if (productImage == null)
                return NotFound();

            return Ok(productImage);
        }
    }
}
