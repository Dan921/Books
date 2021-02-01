using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Insert(TEntity entity);
        Task Delete(Guid id);
        Task Update(TEntity entity);
        Task Save();
    }
}
