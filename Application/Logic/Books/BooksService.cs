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

        public async Task<BookModel> GetBookById(Guid id)
        {
            var book = await bookRepository.GetById(id);
            if (book != null)
            {
                var bookModel = ModelsHelper.GetBookModel(book); 
                return bookModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<BookShortModel>> GetBooks()
        {
            var books = await bookRepository.GetAll();
            var BookShortModels = books.Select(p => ModelsHelper.GetBookShortModel(p));
            return BookShortModels;
        }

        public async Task<bool> InsertBook(BookModel bookModel)
        {
            try
            {
                await bookRepository.Insert(ModelsHelper.GetBookFromModel(bookModel));
                await bookRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookModel> UpdateBook(BookModel bookModel)
        {
            try
            {
                await bookRepository.Update(ModelsHelper.GetBookFromModel(bookModel));
                await bookRepository.Save();
                return bookModel;
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

        public async Task<BookModel> UpdateBookCover(Guid id, IFormFile file)
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
                return ModelsHelper.GetBookModel(book);
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
