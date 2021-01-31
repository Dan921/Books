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
        private readonly IUnitOfWork unitOfWork;

        public BooksService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> IsBookExist(Guid id)
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

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await unitOfWork.BookRepository.Update(book);
                await unitOfWork.Save();
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
                await unitOfWork.BookRepository.Delete(Id);
                await unitOfWork.Save();
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
                var book = await unitOfWork.BookRepository.GetById(id);
                if (book != null)
                {
                    book.CoverImage = ImageConverter.ConvertToByteArray(file);
                    await unitOfWork.BookRepository.Update(book);
                    await unitOfWork.Save();
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
            var book = await unitOfWork.BookRepository.GetById(id);
            if(book == null)
            {
                return null;
            }
            return book.CoverImage;
        }
    }
}
