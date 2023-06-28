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
    public class SubcategoryController : ControllerBase
    {
        private readonly IGenericRepository<SubCategory> subcategoryRepo;

        public SubcategoryController(IGenericRepository<SubCategory> _subcategoryRepo)
        {
            subcategoryRepo = _subcategoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SubCategory>>> GetAllSubCategories()
        {
            return Ok (await subcategoryRepo.GetAllAsync());        
        }

        [HttpGet("{id:int}", Name = "GetOneSubcategoryRoute")]

        public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
        {
            var spec = new GetProductsOfSubcategory(id);
            SubCategory subcategory = await subcategoryRepo.GetEntityWithSpec(spec);

            if (subcategory == null)
            {
                return NotFound("SubCategory Not Found");
            }

            SubcategoryWithProducts subcategoryWithProducts = new SubcategoryWithProducts();
            subcategoryWithProducts.SubCategoryId = id;
            subcategoryWithProducts.CategoryId = subcategory.CategoryId;
            subcategoryWithProducts.CategoryName = subcategory.Category!=null? subcategory.Category.CategoryName:"No Category";
            subcategoryWithProducts.Name = subcategory.SubCategoryName;
            //subcategoryWithProducts.Image = subcategory.SubCategoryImage;
            foreach (var item in subcategory.Products)
            {
                subcategoryWithProducts.ProductsIds.Add(item.Id);
            }

            return Ok(subcategoryWithProducts);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategory subcategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                SubCategory createdSubcategory =await subcategoryRepo.AddAsync(subcategory);
                string url = Url.Link("GetOneSubcategoryRoute", new { id = subcategory.Id });
                return Created(url, createdSubcategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the subcategory.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, SubCategory subcategory)
        {
            if (id != subcategory.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await subcategoryRepo.EditAsync(id, subcategory);
                    return Ok(subcategory);
                }
                catch (DbUpdateException ex)
                {
                    return StatusCode(500, "An error occurred while updating the subcategory.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                var entity = await subcategoryRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound("Subcategory Not Found");
                }

             await subcategoryRepo.DeleteAsync(id);

             return StatusCode(204,"SubCategory Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the subcategory.");
            }
        }
    }
}
