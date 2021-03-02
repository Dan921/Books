using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsUsingFilter(AuthorFilterModel authorSearchModel);
    }
}
