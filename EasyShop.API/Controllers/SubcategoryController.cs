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
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategories()
        {
            return Ok(await subcategoryRepo.GetAllAsync());
        }

        [HttpGet("{id:int}", Name = "GetOneSubcategoryRoute")]

        public async Task<ActionResult<CategoryWithSubcategories>> GetCategory(int id)
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
            subcategoryWithProducts.CategoryName = subcategory.Category.CategoryName;
            subcategoryWithProducts.Name = subcategory.SubCategoryName;
            subcategoryWithProducts.Image = subcategory.SubCategoryImage;
            foreach (var item in subcategory.Products)
            {
                subcategoryWithProducts.ProductsIds.Add(item.Id);
            }

            return Ok(subcategoryWithProducts);
        }


        [HttpPost]
       // public async Task<IActionResult> CreateCategory(SubCategory subcategory)
       // {
       //     if (!ModelState.IsValid)
       //     {
       //         return BadRequest(ModelState);
       //     }

       //     try
       //     {
       ////         SubCategory createdSubCategory = await subcategoryRepo.AddAsync(subcategory);
       //         string url = Url.Link("GetOneSubcategoryRoute", new { id = subcategory.Id });
       ////         return Created(url, createdSubCategory);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, "An error occurred while adding the subcategory.");
       //     }
       // }


        [HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCategory(int id, SubCategory subcategory)
        //{
        //    if (id != subcategory.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            int rowsAffected = await subcategoryRepo.UpdateAsync(id, subcategory);

        //            if (rowsAffected > 0)
        //            {
        //                return Ok("SubCategory Updated Successfully");
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            return StatusCode(500, "An error occurred while updating the subcategory.");
        //        }
        //    }

        //    return BadRequest(ModelState);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var entity = await subcategoryRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound("Subcategory Not Found");
                }

            //    await subcategoryRepo.DeleteAsync(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the subcategory.");
            }
        }
    }
}
