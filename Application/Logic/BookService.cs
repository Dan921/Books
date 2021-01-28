using Application.Models;
using Data.Context;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<bool> BookExist(Guid id)
        {
            var books = await bookRepository.GetBooks();
            return books.Any(b => b.Id == id);
        }

        public async Task<BookDetailModel> GetBookById(Guid id)
        {
            var book = await bookRepository.GetBookById(id);
            var BookDetailModel = new BookDetailModel
            {
                Id = book.Id,
                Name = book.Name,
                LongDescription = book.LongDescription
            };
            return BookDetailModel;
        }

        public async Task<IEnumerable<BookShortModel>> GetBooks()
        {
            var books = await bookRepository.GetBooks();
            var BookShortModels = books.Select(p => new BookShortModel
            {
                Id = p.Id,
                Name = p.Name,
                ShortDescription = p.ShortDescription
            });
            return BookShortModels;
        }

        public async Task InsertBook(Book book)
        {
            try
            {
                await bookRepository.InsertBook(book);
                await bookRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateBook(Book book)
        {
            try
            {
                await bookRepository.UpdateBook(book);
                await bookRepository.Save();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteBook(Guid Id)
        {
            try
            {
                await bookRepository.DeleteBook(Id);
                await bookRepository.Save();
            }
            catch
            {
                throw;
            }
        }
    }
}
