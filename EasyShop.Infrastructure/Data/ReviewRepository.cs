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
    public class ReviewRepository : IReviewRepository
    {
        private readonly StoreContext context;

        public ReviewRepository(StoreContext _context)
        {
            context = _context;
        }

        public async Task<IReadOnlyList<Review>> GetAll()
        {
            return await context.Reviews.ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await context.Reviews.FindAsync(id);
        }

        public async Task AddAsync(Review review)
        {
            // Check if the customer has bought the product
            bool hasBoughtProduct = await context.OrderDetails
                .AnyAsync(od => od.Order.CustomerId == review.CustomerId&& od.ProductId == review.ProductId);

            if (!hasBoughtProduct)
            {
                throw new InvalidOperationException("The customer has not bought this product.");
            }

            // Assign the customer and product IDs
            //review.CustomerId = customerId;
            //review.ProductId = productId;

            // Add the review to the context
            context.Reviews.Add(review);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            // Check if the customer updating the review is the same one who added it
            bool isSameCustomer = await context.Reviews
                .AnyAsync(r => r.ProductId == review.ProductId && r.CustomerId == review.CustomerId);

            if (!isSameCustomer)
            {
                throw new InvalidOperationException("You are not allowed to update this review.");
            }

            context.Entry(review).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            // Check if the customer deleting the review is the same one who added it
            bool isSameCustomer = await context.Reviews
                .AnyAsync(r => r.ProductId == review.ProductId && r.CustomerId == review.CustomerId);

            if (!isSameCustomer)
            {
                throw new InvalidOperationException("You are not allowed to delete this review.");
            }

            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Review>> GetReviewsForProduct(int productId)
        {
            return await context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
        }
    }
}
