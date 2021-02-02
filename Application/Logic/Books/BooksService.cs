using Application.Models;
using Data.Context;
using Data.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository bookRepository;

        public BooksService(IBooksRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<bool> IsBookExist(Guid id)
        {
            var books = await bookRepository.GetAll();
            return books.Any(b => b.Id == id);
        }

        public async Task<BookDetailModel> GetBookById(Guid id)
        {
            var book = await bookRepository.GetById(id);
            if (book != null)
            {
                var bookDetailModel = new BookDetailModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    LongDescription = book.LongDescription,
                    PublishingDate = book.PublishingDate,
                    AuthorFullName = book.Author.FullName
                };
                return bookDetailModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<BookShortModel>> GetBooks()
        {
            var books = await bookRepository.GetAll();
            var BookShortModels = books.Select(p => new BookShortModel
            {
                Id = p.Id,
                Name = p.Name,
                ShortDescription = p.ShortDescription
            });
            return BookShortModels;
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
                    book.CoverImage = CoverImageConverter.ConvertToByteArray(file);
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
            if(book == null)
            {
                return null;
            }
            return book.CoverImage;
        }
    }
}
