using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IGenericRepository<Review> reviewRepo;

        public ReviewController(IGenericRepository<Review> _reviewRepo)
        {
            reviewRepo = _reviewRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Review>>> GetAllReviews() 
        {
            return Ok(await reviewRepo.GetAllAsync());
        }
    }
}
