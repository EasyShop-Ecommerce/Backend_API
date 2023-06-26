using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSellerController : ControllerBase
    {
        private readonly IGenericRepository<ProductSeller> productSellerRepo;
        private readonly IGenericRepository<Product> productRepo;
        public IGenericRepository<Seller> sellerRepo { get; }

        public ProductSellerController(IGenericRepository<ProductSeller> _productSellerRepo, IGenericRepository<Product> _productRepo, IGenericRepository<Seller> _sellerRepo)
        {
            productSellerRepo = _productSellerRepo;
            productRepo = _productRepo;
            sellerRepo = _sellerRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductSellersDTO>>> GetAllStoreProducts()
        {
            IReadOnlyList<ProductSeller> storeproducts = await productSellerRepo.GetAllAsync();
            return Ok(storeproducts.Select(sp => new ProductSellersDTO()
            {
                ProductId = sp.ProductId,
                SellerId = sp.SellerId,               
                ProductQuantity = sp.Quantity,
                Price= sp.Price,
            }));
        }

        [HttpGet("/products/product/{productId}/seller/{sellerId}")]
        public async Task<ActionResult<ProductSellersDTO>> Get(int productId, int sellerId)
        {
            var productSeller = await productSellerRepo.GetByIdAsync(productId, sellerId);

            if (productSeller == null)
            {
                return NotFound();
            }
            ProductSellersDTO productSellerDTO = new ProductSellersDTO();
            productSellerDTO.ProductId = productId;
            productSellerDTO.SellerId = sellerId;
            productSellerDTO.ProductQuantity = productSeller.Quantity;
            productSellerDTO.Price = productSeller.Price;
            return Ok(productSellerDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductSeller>> Post(ProductSeller productSeller)
        {
            if (productSeller == null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productSeller.SellerId <= 0  || productSeller.ProductId <= 0)
            {
                return BadRequest("Invalid SellerId, or ProductId.");
            }

            //bool sellerExists = await sellerRepo.GetByIdAsync(productSeller.SellerId) != null;
            //if (!sellerExists)
            //{
            //    return BadRequest($"Seller with ID {productSeller.SellerId} does not exist.");
            //}

            //bool productExists = await productRepo.GetByIdAsync(productSeller.ProductId) != null;
            //if (!productExists)
            //{
            //    return BadRequest($"Product with ID {productSeller.ProductId} does not exist.");
            //}

            try
            {
                await productSellerRepo.AddAsync(productSeller);
                return CreatedAtAction("Get", new
                {
                    productId = productSeller.ProductId,
                    sellerId = productSeller.SellerId,                  
                }, productSeller);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"An error occurred while adding the product seller: {ex.Message}");
            }
        }

        [HttpPut("product/{productId}/seller/{sellerId}")]
        public async Task<IActionResult> Put(int productId, int sellerId, ProductSeller productSeller)
        {
            if (productId != productSeller.ProductId ||
                sellerId != productSeller.SellerId )
            {
                return BadRequest();
            }

            var existingProductSeller = await productSellerRepo.GetByIdAsync(productId, sellerId);
            if (existingProductSeller == null)
            {
                return NotFound($"Product {productId} with seller {sellerId}  Not Found");
            }

            //existingProductSeller.Quantity = productSeller.Quantity;

            await productSellerRepo.EditAsync(productId,sellerId, productSeller);

            return StatusCode(204, "Product Seller Updated Successfully");
        }

        [HttpDelete("product/{productId}/seller/{sellerId}")]
        public async Task<IActionResult> Delete(int productId, int sellerId, int storeId)
        {
            var storeProduct = await productSellerRepo.GetByIdAsync(productId, sellerId);

            if (storeProduct == null)
            {
                return NotFound();
            }

            await productSellerRepo.DeleteAsync(productId,sellerId);

            return Ok("Seller Product Deleted Successfully");
        }

    }
}
