using Application.Models;
using Data.Context;
using Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BooksQueriesService : IBooksQueriesService
    {
        private readonly IBooksRepository bookRepository;

        public BooksQueriesService(IBooksRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var book = await bookRepository.GetById(id);
            if (book != null)
            {
                return book;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var books = await bookRepository.GetAll();
            return books;
        }

        public async Task<bool> InsertBook(Book book)
        {
            try
            {
                await bookRepository.Insert(book);
                await bookRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await bookRepository.Update(book);
                await bookRepository.Save();
                return book;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteBook(Guid Id)
        {
            try
            {
                await bookRepository.Delete(Id);
                await bookRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Book> UpdateBookCover(Guid id, IFormFile file)
        {
            try
            {
                var book = await bookRepository.GetById(id);
                if (book != null)
                {
                    book.CoverImage = ImageConverter.ConvertToByteArray(file);
                    await bookRepository.Update(book);
                    await bookRepository.Save();
                }
                return book;
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> GetBookCover(Guid id)
        {
            var book = await bookRepository.GetById(id);
            if (book == null)
            {
                return null;
            }
            return book.CoverImage;
        }

        public async Task<bool> AddReview(Guid bookId, BookReview review)
        {
            try
            {
                var book = bookRepository.GetById(bookId);
                if (book != null)
                {
                    await bookRepository.AddReview(bookId, review);
                    await bookRepository.Save();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> SearchBy(string BookName, string authorName, string seriesName, int? year, string[] ganreNames, string[] tagNames)
        {
            var authors = await bookRepository.SearchBy(BookName, authorName, seriesName, year, ganreNames, tagNames);
            return authors;
        }

        public async Task<IEnumerable<Book>> GetTopRated()
        {
            var authors = await bookRepository.GetTopRated();
            return authors;
        }

        public async Task<IEnumerable<Book>> GetTopByNumberOfRatings()
        {
            var authors = await bookRepository.GetTopByNumberOfRatings();
            return authors;
        }
    }
}
