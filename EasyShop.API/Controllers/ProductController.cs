using EasyShop.API.DTOs;
using EasyShop.API.Helpers;
using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Reflection.Metadata.Ecma335;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepo;
        private readonly IWebHostEnvironment environment;

        //private readonly IGenericRepository<Product> productRepo;

        public ProductController(IProductRepository _productRepo, IWebHostEnvironment _environment)
        {
            productRepo = _productRepo;
            environment = _environment;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            IReadOnlyList<Product> products = await productRepo.GetAllProducts();
            return Ok(products.Select(p => new ProductDTO()
            {
                Id = p.Id,
                BrandName = p.BrandName,
                Code=p.Code,
                Description = p.Description,
                Title = p.Title,
                Price = p.Price,
                Qty=p.AvailableQuantity,
                OperatingSystem = p.OperatingSystem ?? null,
                HardDiskSize = p.HardDiskSize ?? null,
                MemoryStorageCapacity = p.MemoryStorageCapacity ?? null,
                SpecialFeatures = p.SpecialFeatures ?? null,
                SubCategory = p.SubCategory != null ? p.SubCategory.SubCategoryName : null,
                SubCategoryId = p.SubCategoryId,
                Category = p.SubCategory != null ? p.SubCategory.Category?.CategoryName : null,
                CategoryId = p.SubCategory != null ? p.SubCategory.Category?.Id : null,
                ShipperId = p.ShipperId,
                SellerId = p.SellerId,
                //Sellers = p.ProductSellers != null ? p.ProductSellers.Select(ps => new ProductSellersDTO
                //{
                //    ProductId = ps.ProductId,
                //    SellerId = ps.SellerId,
                //    ProductQuantity = ps.Quantity,
                //    Price = ps.Price,
                //}).ToList() : null,
                ReviewsAverage = p.ReviewsAverage,
                ReviewsCount = p.ReviewsCount,
                DefaultImage = p.ProductImages?.FirstOrDefault()?.Image != null
                                               ? Convert.ToBase64String(p.ProductImages.FirstOrDefault().Image)
                                                : null
            })) ;
        }

        [HttpGet("{id:int}", Name = "GetOneProductRoute")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //var spec = new GetProductWithReviews(id);
            Product productToReturn = await productRepo.GetProductById(id);
            if (productToReturn == null)
            {
                return NotFound();
            }
            ProductDTO product = new ProductDTO()
            {
                Id = productToReturn.Id,
                BrandName = productToReturn.BrandName,
                Code=productToReturn.Code,
                Description = productToReturn.Description,
                Title = productToReturn.Title,
                Price = productToReturn.Price,
                Qty=productToReturn.AvailableQuantity,
                Material = productToReturn.Material ?? null,
                OperatingSystem = productToReturn.OperatingSystem ?? null,
                HardDiskSize = productToReturn.HardDiskSize ?? null,
                MemoryStorageCapacity = productToReturn.MemoryStorageCapacity ?? null,
                SpecialFeatures = productToReturn.SpecialFeatures ?? null,
                SubCategory = productToReturn.SubCategory != null ? productToReturn.SubCategory.SubCategoryName : null,
                SubCategoryId = productToReturn.SubCategoryId,
                Category = productToReturn.SubCategory != null ? productToReturn.SubCategory.Category?.CategoryName : null,
                CategoryId = productToReturn.SubCategory != null ? productToReturn.SubCategory.Category?.Id : null,
                //Sellers = productToReturn.ProductSellers != null ? productToReturn.ProductSellers.Select(ps => new ProductSellersDTO
                //{
                //    ProductId=id,
                //    SellerId = ps.SellerId,
                //    ProductQuantity = ps.Quantity,
                //    Price = ps.Price,
                //}).ToList() : null,
                ShipperId=productToReturn.ShipperId,
                SellerId=productToReturn.SellerId,
                ReviewsAverage = productToReturn.ReviewsAverage,
                ReviewsCount = productToReturn.ReviewsCount,
                DefaultImage = productToReturn.ProductImages?.FirstOrDefault()?.Image != null
                                               ? Convert.ToBase64String(productToReturn.ProductImages.FirstOrDefault().Image)
                                                : null
            };
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (product == null) return BadRequest();
            if (ModelState.IsValid)
            {
                if (!await productRepo.SubCategoryExists(product.SubCategoryId))
                {
                    ModelState.AddModelError("SubCategoryId", "Invalid SubCategory ID");
                    return BadRequest(ModelState);
                }

                await productRepo.AddProduct(product);
                string url = Url.Link("GetOneProductRoute", new { id = product.Id });
                return Created(url, product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (product == null) return BadRequest();
            if (id != product.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    int rowsAffected = await productRepo.UpdateProduct(id, product);

                    if (rowsAffected > 0)
                    {
                        return Ok(product);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (DbUpdateException ex)
                {
                    return StatusCode(500, "An error occurred while updating the product.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await productRepo.DeleteProduct(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpPut("{productId}/UploadImages/{color}")]
        //public async Task<IActionResult> UploadImages(IFormFileCollection fileCollection, int productId, string color)
        //{
        //    if (fileCollection == null || fileCollection.Count == 0)
        //    {
        //        return BadRequest("No files were uploaded.");
        //    }

        //    if (string.IsNullOrEmpty(color))
        //    {
        //        return BadRequest("Invalid color value.");
        //    }

        //    var images = new List<ProductImage>();

        //    try
        //    {
        //        var product = productRepo.GetProductById(productId);
        //        if (product == null)
        //        {
        //            return NotFound("Product not found");
        //        }
        //        foreach (var file in fileCollection)
        //        {

        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                await file.CopyToAsync(memoryStream);
        //                var image = new ProductImage
        //                {
        //                    ProductId = productId,
        //                    Color = color,
        //                    Image = memoryStream.ToArray()
        //                };
        //                images.Add(image);
        //            }
        //        }

        //        await productRepo.AddRangeAsync(images);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, "An error occurred during image upload.");
        //    }

        //    return Ok(images);
        //}


        [HttpPut("{productId}/UploadImages/{color}")]
        public async Task<IActionResult> UploadImages(IFormFileCollection fileCollection, int productId, string color)
        {
            int passcount = 0;
            int errorcount = 0;
            var images = new List<ProductImage>();

            try
            {
                foreach (var file in fileCollection)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var image = new ProductImage
                        {
                            ProductId = productId,
                            Color = color,
                            Image = memoryStream.ToArray()
                        };
                        images.Add(image);
                        passcount++;
                    }
                }

                await productRepo.AddRangeAsync(images);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An Error Occurred");
            }

            return Ok(images);
        }


        [HttpGet("{productId}/images/{color}")]
        public async Task<IActionResult> GetProductImages(int productId, string color)
        {
            List<string> imagesUrls = new List<string>();
            try
            {
                var productImages = productRepo.GetProductImages(productId, color);

                if (productImages != null && productImages.Count > 0)
                {
                    productImages.ForEach(item =>
                    {
                        imagesUrls.Add(Convert.ToBase64String(item.Image));
                    });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An Error Occurred");
            }
            return Ok(imagesUrls);
        }


        //[HttpGet("{productId}/images/{color}")]
        //public async Task<IActionResult> GetProductImages(int productId, string color)
        //{
        //    List<string> imagesUrls = new List<string>();

        //    if (productId <= 0)
        //    {
        //        return BadRequest("Invalid productId");
        //    }

        //    if (string.IsNullOrEmpty(color))
        //    {
        //        return BadRequest("Color parameter is required");
        //    }

        //    try
        //    {
        //        var product = await productRepo.GetProductById(productId);
        //        if (product == null)
        //        {
        //            return NotFound("Product not found");
        //        }

        //        var productImages = productRepo.GetProductImages(productId, color);

        //        if (productImages != null && productImages.Count > 0)
        //        {
        //            productImages.ForEach(item =>
        //            {
        //                imagesUrls.Add(Convert.ToBase64String(item.Image));
        //            });
        //        }
        //        else
        //        {
        //            return NotFound("No images found for the specified product and color");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }

        //    return Ok(imagesUrls);
        //}


        [NonAction]
        private string GetFilePath(int productId)
        {
            return environment.WebRootPath + "\\Uploads\\Products\\" + productId.ToString();
        }

        #region Trying Adding Product Image With Product Data

        //[HttpPost]
        //public async Task<ActionResult<ProductWithCategoryAndReviewsDTO>> Post([FromForm] PostProductDTO model)
        //{
        //    if (model is null)
        //        return BadRequest();

        //    var product = new Product()
        //    {
        //        BrandName = model.BrandName,
        //        Title = model.Title,
        //        Description = model.Description,
        //        Material = model.Material,
        //        //Price = model.Price,
        //        OperatingSystem = model.OperatingSystem,
        //        HardDiskSize = model.HardDiskSize,
        //        MemoryStorageCapacity = model.MemoryStorageCapacity,
        //        SpecialFeatures = model.SpecialFeatures,
        //        SubCategoryId = model.SubCategoryId,
        //        ShipperId=model.ShipperId,
        //    };


        //    product = await productRepo.AddProduct(product);

        //    string wwwPath = environment.WebRootPath;

        //    if (string.IsNullOrEmpty(wwwPath))
        //        return StatusCode(500, $"This Path {wwwPath} does not exist");

        //    if (model.ImagesOfProduct is null)
        //        return BadRequest("You Must Enter At List One Image");


        //    var images = await FileUploader.UploadProductImages(product.Id, model.ImagesOfProduct, wwwPath);
        //    var mainImage = images.FirstOrDefault();
        //    mainImage.IsDefault = true;

        //    await productRepo.AddRangeAsync(images);

        //    var createdProduct = new ProductWithCategoryAndReviewsDTO
        //    {
        //        Id = product.Id,
        //        BrandName = product.BrandName,
        //        Title = product.Title,
        //        Description = product.Description,
        //        Material = product.Material,
        //        //Price = product.Price,
        //        OperatingSystem = product.OperatingSystem,
        //        HardDiskSize = product.HardDiskSize,
        //        MemoryStorageCapacity = product.MemoryStorageCapacity,
        //        SpecialFeatures = product.SpecialFeatures,
        //        SubCategory = product.SubCategory != null ? product.SubCategory.SubCategoryName : null,
        //        Category = product.SubCategory != null ? product.SubCategory.Category?.CategoryName : null,
        //        ReviewsAverage = product.ReviewsAverage,
        //        ReviewsCount = product.ReviewsCount,
        //    };
        //    createdProduct.MainImage = mainImage.ImagePath;

        //    return Ok(createdProduct);
        //}
        #endregion
        #region Tring Images Adding
        //[HttpPut("{productId}/UploadImages")]
        //public async Task<IActionResult> UploadImages(IFormFileCollection fileCollection, int productId)
        //{
        //    int passcount = 0;
        //    int errorcount = 0;
        //    try
        //    {
        //        string filePath = GetFilePath(productId);
        //        if (!System.IO.Directory.Exists(filePath))
        //        {
        //            System.IO.Directory.CreateDirectory(filePath);
        //        }

        //        foreach (var file in fileCollection)
        //        {
        //            string imagePath = filePath + "\\" + file.FileName;

        //            if (System.IO.File.Exists(imagePath))
        //            {
        //                System.IO.File.Delete(imagePath);
        //            }

        //            using (FileStream stream = System.IO.File.Create(imagePath))
        //            {
        //                await file.CopyToAsync(stream);
        //                passcount++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorcount++;
        //        return StatusCode(500, $"An Error Occured {errorcount}");
        //    }
        //    return Ok($" {passcount} Images Uploaded Successfully and {errorcount} Failed");
        //}

        //[HttpGet("{productId}/images")]
        //public async Task<IActionResult> GetProductImages(int productId)
        //{
        //    List<string> imagesUrls= new List<string>();
        //    string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        //    try
        //    {
        //        string filePath=GetFilePath(productId);

        //        if (System.IO.Directory.Exists(filePath)) 
        //        { 
        //            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
        //            FileInfo[] fileInfos = directoryInfo.GetFiles();
        //            foreach (FileInfo fileInfo in fileInfos)
        //            {
        //                string filename = fileInfo.Name;
        //                string imagePath = filePath + "\\" + filename;  
        //                if(System.IO.File.Exists (imagePath))
        //                {
        //                    imagesUrls.Add(hostUrl + "/Uploads/Products/" + productId.ToString()+"/"+filename);
        //                }
        //            }
        //        }
        //    }
        //    catch( Exception ex ) 
        //    {
        //        return StatusCode(500,"An Error Occured");
        //    }
        //    return Ok(imagesUrls);
        //}
        //[HttpGet("{productId}/images/color/{color}")]
        //public async Task<IActionResult> GetProductImages(int productId, string color)
        //{
        //    try
        //    {
        //        var productImages = productRepo.GetProductImages(productId, color);

        //        if (productImages != null && productImages.Count > 0)
        //        {
        //            var fileContentsList = new List<FileContentResult>();

        //            foreach (var item in productImages)
        //            {
        //                var fileContent = new FileContentResult(item.Image, "image/jpeg");
        //                fileContentsList.Add(fileContent);
        //            }

        //            var archiveStream = new MemoryStream();
        //            using (var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true))
        //            {
        //                for (int i = 0; i < fileContentsList.Count; i++)
        //                {
        //                    var fileContent = fileContentsList[i];
        //                    var entryName = $"image_{i + 1}.jpg";
        //                    var entry = zipArchive.CreateEntry(entryName, CompressionLevel.NoCompression);

        //                    using (var entryStream = entry.Open())
        //                    {
        //                        await entryStream.WriteAsync(fileContent.FileContents);
        //                    }
        //                }
        //            }

        //            archiveStream.Seek(0, SeekOrigin.Begin);
        //            return File(archiveStream, "application/zip", "product_images.zip");
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An Error Occurred");
        //    }
        //}
        #endregion
    }
}
