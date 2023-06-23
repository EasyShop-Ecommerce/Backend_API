using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
    public class StoreProductRepository : IStoreProductRepository
    {
        private readonly DBContext _dbContext;

        public StoreProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<StoreProduct>> GetAll()
        {
            return await _dbContext.StoreProducts.ToListAsync();
        }

        public async Task<StoreProduct> GetByIdAsync(int productId, int sellerId, int storeId)
        {
            return await _dbContext.StoreProducts
                .SingleOrDefaultAsync(sp =>
                    sp.ProductId == productId &&
                    sp.SellerId == sellerId &&
                    sp.StoreId == storeId);
        }

        public async Task AddAsync(StoreProduct storeProduct)
        {
            _dbContext.StoreProducts.Add(storeProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(StoreProduct storeProduct)
        {
            _dbContext.Entry(storeProduct).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            //_dbContext.StoreProducts.Update(storeProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(StoreProduct storeProduct)
        {
            _dbContext.StoreProducts.Remove(storeProduct);
            await _dbContext.SaveChangesAsync();
        }   
    }
}
