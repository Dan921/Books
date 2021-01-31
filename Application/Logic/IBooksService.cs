using Application.Models;
using Data.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IBooksService
    {
        Task<IEnumerable<BookShortModel>> GetBooks();
        Task<BookDetailModel> GetBookById(Guid id);
        Task<bool> InsertBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(Guid Id);
        Task<bool> IsBookExist(Guid id);
        Task<Book> UpdateBookCover(Guid id, IFormFile file);
        Task<byte[]> GetBookCover(Guid id);
    }
}
