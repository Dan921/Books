using Application.Models;
using Data.Context;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> BookExist(Guid id)
        {
            var books = await unitOfWork.BookRepository.GetAll();
            return books.Any(b => b.Id == id);
        }

        public async Task<BookDetailModel> GetBookById(Guid id)
        {
            var book = await unitOfWork.BookRepository.GetById(id);
            if (book != null)
            {
                var bookDetailModel = new BookDetailModel
                {
                    Id = book.Id,
                    Name = book.Name,
                    LongDescription = book.LongDescription
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
            var books = await unitOfWork.BookRepository.GetAll();
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
                await unitOfWork.BookRepository.Insert(book);
                await unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBook(Book book)
        {
            if(!await BookExist(book.Id))
            {
                return false;
            }
            try
            {
                await unitOfWork.BookRepository.Update(book);
                await unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(Guid Id)
        {
            try
            {
                await unitOfWork.BookRepository.Delete(Id);
                await unitOfWork.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
