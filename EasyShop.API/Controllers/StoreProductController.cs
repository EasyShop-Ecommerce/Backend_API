using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreProductsController : ControllerBase
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IGenericRepository<Product> _productRepo;

        public IGenericRepository<Seller> _sellerRepo { get; }
        public IGenericRepository<Store> _storeRepo { get; }

        public StoreProductsController(IStoreProductRepository storeProductRepository,
            IGenericRepository<Seller> sellerRepo,
            IGenericRepository<Store> storeRepo,
            IGenericRepository<Product> productRepo)
        {
            _storeProductRepository = storeProductRepository;
            _sellerRepo = sellerRepo;
            _storeRepo = storeRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StoreProduct>>> GetAllStoreProducts()
        {
            IReadOnlyList<StoreProduct> storeproducts = await _storeProductRepository.GetAll();
            return Ok(storeproducts.Select(sp => new StoreProductDTO()
            {
              ProductId = sp.ProductId,
              SellerId = sp.SellerId,
              StoreId=sp.StoreId,
              Quantity = sp.Quantity,
            }));
        }

        [HttpGet("{productId}/{sellerId}/{storeId}")]
        public async Task<ActionResult<StoreProduct>> Get(int productId, int sellerId, int storeId)
        {
            var storeProduct = await _storeProductRepository.GetByIdAsync(productId, sellerId, storeId);

            if (storeProduct == null)
            {
                return NotFound();
            }
            StoreProductDTO storeProductDTO = new StoreProductDTO();
            storeProductDTO.ProductId = productId;
            storeProductDTO.SellerId=sellerId;
            storeProductDTO.StoreId=storeId;
            storeProductDTO.Quantity = storeProduct.Quantity;
            return Ok(storeProductDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StoreProduct>> Post(StoreProduct storeProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (storeProduct.SellerId <= 0 || storeProduct.StoreId <= 0 || storeProduct.ProductId <= 0)
            {
                return BadRequest("Invalid SellerId, StoreId, or ProductId.");
            }

            bool sellerExists = await _sellerRepo.GetByIdAsync(storeProduct.SellerId) != null;
            if (!sellerExists)
            {
                return BadRequest($"Seller with ID {storeProduct.SellerId} does not exist.");
            }

            bool storeExists = await _storeRepo.GetByIdAsync(storeProduct.StoreId) != null;
            if (!storeExists)
            {
                return BadRequest($"Store with ID {storeProduct.StoreId} does not exist.");
            }

            bool productExists = await _productRepo.GetByIdAsync(storeProduct.ProductId) != null;
            if (!productExists)
            {
                return BadRequest($"Product with ID {storeProduct.ProductId} does not exist.");
            }

            try
            {
                await _storeProductRepository.AddAsync(storeProduct);
                return CreatedAtAction("Get", new
                {
                    productId = storeProduct.ProductId,
                    sellerId = storeProduct.SellerId,
                    storeId = storeProduct.StoreId
                }, storeProduct);
            }
             catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the store product: {ex.Message}");
            }
        }

        [HttpPut("{productId}/{sellerId}/{storeId}")]
        public async Task<IActionResult> Put(int productId, int sellerId, int storeId, StoreProduct storeProduct)
        {
            if (productId != storeProduct.ProductId ||
                sellerId != storeProduct.SellerId ||
                storeId != storeProduct.StoreId)
            {
                return BadRequest();
            }

            // Check if the store product exists
            var existingStoreProduct = await _storeProductRepository.GetByIdAsync(productId, sellerId, storeId);
            if (existingStoreProduct == null)
            {
                return NotFound("Store product Not Found");
            }

            // Update the store product
            existingStoreProduct.Quantity = storeProduct.Quantity;

            await _storeProductRepository.UpdateAsync(existingStoreProduct);

            return StatusCode(204,"Store Product Updated Successfully");
        }

        [HttpDelete("{productId}/{sellerId}/{storeId}")]
        public async Task<IActionResult> Delete(int productId, int sellerId, int storeId)
        {
            var storeProduct = await _storeProductRepository.GetByIdAsync(productId, sellerId, storeId);

            if (storeProduct == null)
            {
                return NotFound();
            }

            await _storeProductRepository.DeleteAsync(storeProduct);

            return NoContent();
        }
    }

    //Possible Needed Functionalities:
    //Get All Products Of Specific Seller
    //Get All Products Of Specific Store

    //In update , i update the quantity of product in specific store and for specific user
    //Can i update the store id, if i want to transfer products for specific seller to another store

    //  return StatusCode(204,"Store Product Updated Successfully"); //The Message doesnot return
}
