using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IBookService
    {
        Task<IEnumerable<BookShortModel>> GetBooks();
        Task<BookDetailModel> GetBookById(Guid id);
        Task InsertBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(Guid Id);
        Task<bool> BookExist(Guid id);
    }
}
