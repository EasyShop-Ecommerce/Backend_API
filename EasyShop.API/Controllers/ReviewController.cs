using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewController(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await reviewRepository.GetAll();
            return Ok(reviews);
        }

        //get by id needs productid, customer id (edit)
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetReviewById(int id)
        //{
        //    var review = await reviewRepository.GetByIdAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(review);
        //}

        [HttpPost]
        public async Task<IActionResult> AddReview(Review review)
        {
            try
            {
                await reviewRepository.AddAsync(review);
                return Created("Review Added Successfully",review);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview( Review review)
        {
            try
            {
                await reviewRepository.UpdateAsync(review);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReview(Review review)
        {           
            if (review == null)
            {
                return NotFound();
            }

            try
            {
                await reviewRepository.DeleteAsync(review);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/reviews/product/{productId}")]
        public async Task<IActionResult> GetReviewsForProduct(int productId)
        {
            var reviews = await reviewRepository.GetReviewsForProduct(productId);
            return Ok(reviews);
        }
    }
}
