using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Customer> _customerRepository;


        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllAsync();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the reviews.");
            }
        }

        [HttpGet("product/{productId:int}/customer/{customerId:int}",Name = "GetOneReviewRoute")]
        public async Task<IActionResult> GetReview(int productId, int customerId)
        {
            try
            {
                var review = await _reviewRepository.GetByIdAsync(productId, customerId);
                if (review == null)
                {
                    return NotFound();
                }

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the review.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review)
        {
            if(review == null) return BadRequest();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _reviewRepository.AddAsync(review);
                string url = Url.Link("GetOneReviewRoute", new {productId=review.ProductId,customerId=review.CustomerId });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the review.");
            }
        }

        [HttpPut("product/{productId:int}/customer/{customerId:int}")]
        public async Task<IActionResult> UpdateReview(int productId, int customerId, Review review)
        {
            try
            {
                if (review==null) return BadRequest();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _reviewRepository.UpdateAsync(review, productId, customerId);

                //if (result == "Entity not found.")
                //{
                //    return NotFound("The specified product review does not exist.");
                //}

                return Ok(review);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while updating the review.");
            }
        }


        [HttpDelete("product/{productId:int}/customer/{customerId:int}")]
        public async Task<IActionResult> DeleteReview(int productId, int customerId)
        {
            try
            {
                var review = await _reviewRepository.DeleteAsync(productId, customerId);
                if (review == null)
                {
                    return NotFound();
                }

                return Ok(review);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the review.");
            }
        }


        [HttpGet("/reviews/product/{productId:int}")]
        public async Task<IActionResult> GetReviewsForProduct(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Invalid productId. The productId must be a positive integer.");
            }

            try
            {
                var reviews = await _reviewRepository.GetReviewsForProduct(productId);

                if (reviews == null || reviews.Count == 0)
                {
                    return NotFound("No reviews found for the specified productId.");
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {         
                return StatusCode(500, "An error occurred while retrieving reviews for the product. Please try again later.");
            }
        }
  
    }
}
