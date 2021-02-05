using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> SearchByName(string name);
        Task<IEnumerable<Author>> SearchByDate(DateTime? birthDate, DateTime? deathDate);
    }
}
