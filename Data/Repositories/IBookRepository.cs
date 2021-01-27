using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookById(Guid id);
        Task InsertBook(Book book);
        Task UpdateBook(Book book);
        Task Save();
        Task<bool> BookExist(Guid id);
    }
}
