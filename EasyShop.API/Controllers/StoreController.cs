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
            IReadOnlyList<Store> stores = await storeRepo.GetAllAsync();
            return Ok();
        }

       
        [HttpGet("{id:int}",Name = "GetOneStoreRoute")]
        public async Task<ActionResult<StoreWithStoreProducts>> GetStoreWithProducts(int id)
        {
            var spec = new GetStoreWithProducts(id);
            Store store = await storeRepo.GetEntityWithSpec(spec);

            if (store == null)
            {
                return NotFound("Store Not Found");
            }

            StoreWithStoreProducts storeWithStoreProducts = new StoreWithStoreProducts();
            storeWithStoreProducts.StoreId = id;
            storeWithStoreProducts.StoreLocation = store.Location;
            foreach (var item in store.StoreProducts)
            {
                storeWithStoreProducts.StoreProducts.Add(new StoreProductDTO 
                {
                    ProductId=item.ProductId,
                    SellerId=item.SellerId,
                    StoreId=item.StoreId,
                    Quantity=item.Quantity
                });
            }
            return Ok(storeWithStoreProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await storeRepo.AddAsync(store);
                string url = Url.Link("GetOneStoreRoute", new { id = store.Id });
                return Created(url, "Store Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the store.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, Store store)
        {
            if(store== null) return BadRequest();
            if (id != store.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    string result = await storeRepo.EditAsync(id, store);

                    if (result == "Entity not found.")
                    {
                        return NotFound("The specified store does not exist.");
                    }

                    return Ok("Store Updated Successfully");
                }
                catch (DbUpdateException ex)
                {
                    return StatusCode(500, "An error occurred while updating the store.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                var entity = await storeRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound("Store Not Found");
                }

             await storeRepo.DeleteAsync(id);

                return StatusCode(204,"Store Created Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the store.");
            }
        }
    }
}
