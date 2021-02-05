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
        Task<BookModel> GetBookById(Guid id);
        Task<bool> InsertBook(BookModel book);
        Task<BookModel> UpdateBook(BookModel book);
        Task<bool> DeleteBook(Guid Id);
        Task<bool> IsBookExist(Guid id);
        Task<BookModel> UpdateBookCover(Guid id, IFormFile file);
        Task<byte[]> GetBookCover(Guid id);
    }
}
