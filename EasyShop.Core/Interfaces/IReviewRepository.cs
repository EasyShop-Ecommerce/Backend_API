using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
    public interface IReviewRepository:IGenericRepository<Review>
    {      
        Task<IReadOnlyList<Review>> GetReviewsForProduct(int productId);
    }
}
