﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Insert(TEntity entity);
        Task Delete(Guid id);
        Task Update(TEntity entity);
        Task Save();
    }
}
