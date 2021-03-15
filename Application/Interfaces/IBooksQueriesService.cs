using Application.Models;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBooksQueriesService
    {
        Task<IQueryable<Book>> GetBooks(BookFilterModel bookSearchModel, IList<string> roles);
        Task<Book> GetBookById(Guid id);
        Task<bool> InsertBook(Book book);
        Task<Book> UpdateBook(Book book, IList<string> roles);
        Task<bool> DeleteBook(Guid Id);
        Task<BookCover> UpdateBookCover(Guid id, IFormFile file);
        Task<byte[]> GetBookCover(Guid id);
        Task<bool> DeleteBookCover(Guid Id);
        Task<bool> IsBookCoverExist(Guid Id);
        Task<bool> AddReview(Guid bookId, BookReview review);
        Task<IEnumerable<BookReview>> GetReviewsByBookId(Guid bookId);
        Task<bool> ToRentBook(BookRent bookRent);
    }
}
