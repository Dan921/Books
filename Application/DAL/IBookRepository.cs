using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.DAL
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<Book> GetBooks();
        Task<Book>GetBookByID(Guid id);
        void InsertBook(Book book);
        void DeleteBook(Guid id);
        void UpdateBook(Book book);
        void Save();
        bool BookExist(Guid id);
    }
}
