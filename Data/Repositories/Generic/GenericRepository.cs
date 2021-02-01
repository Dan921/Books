using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected BooksContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(BooksContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var data = await dbSet.ToListAsync();
            return data;
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
