using EasyShop.API.DTOs;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IGenericRepository<Store> storeRepo;

        public StoreController(IGenericRepository<Store> _storeRepo)
        {
            storeRepo = _storeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Store>>> GetAllStores() 
        {
            return Ok(await storeRepo.GetAllAsync());
        }

       
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StoreWithStoreProducts>> GetStoreWithProducts(int id)
        {
            var spec = new GetStoreWithProducts(id);
            Store store = await storeRepo.GetEntityWithSpec(spec);

            if (store == null)
            {
                return NotFound("Store Not Found");
            }

            StoreWithStoreProducts storeWithStoreProducts = new StoreWithStoreProducts();
            storeWithStoreProducts.StoreId = store.Id;
            storeWithStoreProducts.StoreLocation = store.Location;
            foreach (var item in store.StoreProducts)
            {
                storeWithStoreProducts.StoreProducts.Add(new StoreProductDTO 
                {
                    ProductId=item.ProductId,
                    SellerId=item.SellerId,
                    Quantity=item.Quantity
                });
            }
            return Ok(storeWithStoreProducts);
        }
    }
}
