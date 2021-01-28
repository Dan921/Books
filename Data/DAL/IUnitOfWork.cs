using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Book> BookRepository { get; }
        Task Save();
    }
}
