using EasyShop.Core.Entities;
using EasyShop.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Core.Interfaces
{
    public interface IGenericRepository<T>
    {
		//Methods Signature that deals with all classes

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id1,int id2);

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<T> AddAsync(T t);

		Task<T> EditAsync(int Id1,int Id2,T t);
        Task<T> EditAsync(int Id, T t);

        Task<T> DeleteAsync(int Id);
        Task<T> DeleteAsync(int Id1, int Id2);

        //Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(params int[] ids);

        Task<T> DeleteAsync(params int[] ids);

        Task<T> UpdateAsync(T entity, params int[] ids);
    }
}
