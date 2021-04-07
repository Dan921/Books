using Data.Context;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected LibraryContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(LibraryContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public Task<IQueryable<TEntity>> GetAll()
        {
            IQueryable<TEntity> data = dbSet;
            return Task.FromResult(data);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var data = await dbSet.FindAsync(id);
            return data;
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            if(entityToDelete != null)
            {
                dbSet.Remove(entityToDelete);
            }
        }

        public async Task Update(TEntity entityToUpdate)
        {
            await Task.Run(() =>
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
