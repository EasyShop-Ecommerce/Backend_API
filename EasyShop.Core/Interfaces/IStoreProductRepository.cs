using EasyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
    public interface IStoreProductRepository
    {
        Task<IReadOnlyList<StoreProduct>> GetAll();

        Task<StoreProduct> GetByIdAsync(int productId, int sellerId, int storeId);

        Task AddAsync(StoreProduct storeProduct);

        Task UpdateAsync(StoreProduct storeProduct);

        Task DeleteAsync(StoreProduct storeProduct);

        //Task<IReadOnlyList<StoreProduct>> GetSellerProducts(int sellerId);
    }
}
