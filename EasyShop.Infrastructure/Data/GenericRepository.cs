using EasyShop.Core.Entities;
using EasyShop.Core.Interfaces;
using EasyShop.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private DBContext Context;
        public GenericRepository(DBContext dBContext)
        {
            Context = dBContext;
        }
        public async Task<string> AddAsync(T t)
        {
            await Context.Set<T>().AddAsync(t);
            int row = Context.SaveChanges();
            return ($"NO.Rows is affected = {row}");
        }

        public async Task<T> DeleteAsync(int Id)
        {
            int row = 0;
            T c= await Context.Set<T>().FindAsync(Id);
            if(c != null)
            {
                Context.Set<T>().Remove(c);
                row = Context.SaveChanges();
                await Console.Out.WriteLineAsync($"Deleted Row {row}");
                return c;
            }

            return c;
        }

        public async Task<T> DeleteAsync(int Id1, int Id2)
        {
            int row = 0;
            T c = await Context.Set<T>().FindAsync(Id1,Id2);
            if (c != null)
            {
                Context.Set<T>().Remove(c);
                row = Context.SaveChanges();
                await Console.Out.WriteLineAsync($"Deleted Row {row}");
                return c;
            }

            return c;
        }

        public async Task<string> EditAsync(int Id, T t)
        {

          T c = await Context.Set<T>().FindAsync(Id);
          Context.Entry(c).State = EntityState.Modified;
          int row = Context.SaveChanges();
          return ($"NO.Rows is affected = {row}");

        }

        public async Task<string> EditAsync(int Id1, int Id2, T t)
        {
            T c = await Context.Set<T>().FindAsync(Id1,Id2);
            Context.Entry(c).State = EntityState.Modified;
            int row = Context.SaveChanges();
            return ($"NO.Rows is affected = {row}");

        private readonly DBContext context;

        public GenericRepository(DBContext _context)
        {
            context = _context;

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
<<<<<<< HEAD
            return await Context.Set<T>().ToListAsync();
=======
            return await context.Set<T>().ToListAsync();
>>>>>>> 62d94c05b3a302d89c998bdd93005603b7e1c2d6
        }

        public async Task<T> GetByIdAsync(int id)
        {
<<<<<<< HEAD
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id1, int id2)
        {
            return await Context.Set<T>().FindAsync(id1,id2);
=======
            return await context.Set<T>().FindAsync(id);
>>>>>>> 62d94c05b3a302d89c998bdd93005603b7e1c2d6
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
<<<<<<< HEAD
           return await ApplySpecification(spec).FirstOrDefaultAsync();
=======
            return await ApplySpecification(spec).FirstOrDefaultAsync();
>>>>>>> 62d94c05b3a302d89c998bdd93005603b7e1c2d6
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
<<<<<<< HEAD

        }
        
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), spec);
=======
        }

        private IQueryable<T> ApplySpecification(ISpecification<T>spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);  
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the entity.", ex);
            }
        }

        public async Task<int> UpdateAsync(int id, T entity)
        {
            var entry = context.Entry(entity);
            entry.State = EntityState.Modified;

            try
            {
                int rowsAffected = await context.SaveChangesAsync();
                return rowsAffected;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the entity.", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the entity.", ex);
            }
>>>>>>> 62d94c05b3a302d89c998bdd93005603b7e1c2d6
        }
    }
}
