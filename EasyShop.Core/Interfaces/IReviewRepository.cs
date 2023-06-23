using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<IReadOnlyList<Review>> GetAll();

        Task<Review> GetByIdAsync(int id);

        Task AddAsync(Review review);

        Task UpdateAsync(Review review);

        Task DeleteAsync(Review review);

        Task<IReadOnlyList<Review>> GetReviewsForProduct(int productId);
    }
}
