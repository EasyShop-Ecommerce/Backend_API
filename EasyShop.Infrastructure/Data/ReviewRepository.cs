using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
    public class ReviewRepository :GenericRepository<Review> ,IReviewRepository
    {
        private readonly DBContext context;

        public ReviewRepository(DBContext _context):base(_context)
        {
            context=_context;
        }     
        public async Task<IReadOnlyList<Review>> GetReviewsForProduct(int productId)
        {
            return await context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }
    }
}
