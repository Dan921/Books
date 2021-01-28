using Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(Guid id);
        Task InsertBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(Guid id);
        Task Save();
    }
}
