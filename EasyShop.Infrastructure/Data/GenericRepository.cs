﻿using EasyShop.Core.Entities;
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
        //public async Task<string> AddAsync(T t)
        //{
        //    await Context.Set<T>().AddAsync(t);
        //    int row = Context.SaveChanges();
        //    return ($"NO.Rows is affected = {row}");
        //}

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);
                await Context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ;
            }
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

        public async Task<T> EditAsync(int Id, T t)
        {

          T c = await Context.Set<T>().FindAsync(Id);
          Context.Entry(c).CurrentValues.SetValues(t); 
          await Context.SaveChangesAsync();
          return t;

        }

        public async Task<T> EditAsync(int Id1, int Id2, T t)
        {
            T c = await Context.Set<T>().FindAsync(Id1, Id2);
            Context.Entry(c).CurrentValues.SetValues(t);
            await Context.SaveChangesAsync();
            return t;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(int id1, int id2)
        {
            return await Context.Set<T>().FindAsync(id1,id2);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), spec);
        }

        public async Task<T> GetByIdAsync(params int[] ids)
        {
            return await Context.Set<T>().FindAsync(ids);
        }

        public async Task<T> DeleteAsync(params int[] ids)
        {
            int row = 0;
            T entity = await Context.Set<T>().FindAsync(ids);
            if (entity != null)
            {
                Context.Set<T>().Remove(entity);
                row = Context.SaveChanges();
                await Console.Out.WriteLineAsync($"Deleted Row {row}");
                return entity;
            }

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, params int[] ids)
        {       
            T existingEntity = await Context.Set<T>().FindAsync(ids[0], ids[1]);
            if (existingEntity != null)
            {
                Context.Entry(existingEntity).CurrentValues.SetValues(entity);
                int rowsAffected = await Context.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }






        



        //public async Task DeleteAsync(T entity)
        //{
        //    try
        //    {
        //        context.Set<T>().Remove(entity);
        //        await context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while deleting the entity.", ex);
        //    }

        //}
    }
}
