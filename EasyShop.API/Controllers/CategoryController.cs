﻿using EasyShop.API.DTOs;
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
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> categoryRepo;
        private readonly IWebHostEnvironment environment;

        public CategoryController(IGenericRepository<Category> _categoryRepo, IWebHostEnvironment _environment)
        {
            categoryRepo = _categoryRepo;
            environment = _environment;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategories() 
        {
            return Ok(await categoryRepo.GetAllAsync());
        }

        [HttpGet("{id:int}", Name = "GetOneCategoryRoute")]

        public async Task<ActionResult<CategoryWithSubcategories>> GetCategory(int id)
        {
            var spec = new GetSubcategoriesOfCategory(id);
            Category category = await categoryRepo.GetEntityWithSpec(spec);

            if (category == null)
            {
                return NotFound("Category Not Found"); 
            }

            CategoryWithSubcategories categoryWithSubcategories = new CategoryWithSubcategories();
            categoryWithSubcategories.CategoryId = id;
            categoryWithSubcategories.Name = category.CategoryName;
            categoryWithSubcategories.Image = category.CategoryImage;

            foreach (var item in category.SubCategories)
            {
                categoryWithSubcategories.Subcategories.Add(item.SubCategoryName);
            }

            return Ok(categoryWithSubcategories);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Category createdCategory = await categoryRepo.AddAsync(category);
                string url = Url.Link("GetOnecategoryRoute", new { id =category.Id });
                return Created(url, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the category.");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    int rowsAffected = await categoryRepo.UpdateAsync(id,category);

                    if (rowsAffected > 0)
                    {
                        return Ok("Category Updated Successfully");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateException ex)
                {
                    return StatusCode(500, "An error occurred while updating the category.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await categoryRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                await categoryRepo.DeleteAsync(entity);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            bool result = false;
            try
            {
                var uploadedFiles = Request.Form.Files;
                foreach (IFormFile item in uploadedFiles)
                {
                    string filename = item.FileName;
                    string filepath = GetFilePath(filename);

                    if(!System.IO.Directory.Exists(filepath))
                    {
                        System.IO.Directory.CreateDirectory(filepath);
                    }
                    string imagepath = filepath + "\\image.jpg";

                    if(System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using(FileStream stream=System.IO.File.Create(imagepath))
                    {
                        await item.CopyToAsync(stream);
                        result = true;
                    }
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [NonAction]
        private string GetFilePath(string filename)
        {
            return environment.WebRootPath+"\\Uploads\\Category\\"+ filename;
        }

        private string GetImageByCategory(int categoryId )
        {
             string ImageUrl=string.Empty;
            return ImageUrl;
        }

    }
}
