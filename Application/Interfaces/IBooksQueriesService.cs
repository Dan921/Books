using Application.Models;
using Data.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public interface IBooksQueriesService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(Guid id);
        Task<bool> InsertBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(Guid Id);
        Task<Book> UpdateBookCover(Guid id, IFormFile file);
        Task<byte[]> GetBookCover(Guid id);
        Task<IEnumerable<Book>> SearchBy(string BookName, string authorName, string seriesName, int? year, string[] ganreNames, string[] tagNames);
        Task<IEnumerable<Book>> GetTopRated();
        Task<IEnumerable<Book>> GetTopByNumberOfRatings();
        Task<bool> AddReview(Guid bookId, BookReview review);
    }
}
