﻿using Application.Interfaces;
using Application.Models;
using Data.Context;
using Data.Interfaces;
using Data.Models;
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
        private readonly IBookCoverRepository bookCoverRepository;

        public BooksQueriesService(IBooksRepository bookRepository, IBookCoverRepository bookCoverRepository)
        {
            this.bookRepository = bookRepository;
            this.bookCoverRepository = bookCoverRepository;
        }

        public async Task<Book> GetBookById(Guid id)
        {
            var book = await bookRepository.GetById(id);
            return null;
        }

        public async Task<IQueryable<Book>> GetBooks(BookFilterModel bookSearchModel, IList<string> roles)
        {
            var books = await bookRepository.GetBooksUsingFilter(bookSearchModel);

            var StatusesAvailableChecker = new List<string>() { "на рассмотрении", "опубликованно", "снято с публикации" };

            if (!(roles.Contains("Администратор") || roles.Contains("Писатель")))
            {
                if (roles.Contains("Проверяющий"))
                {
                    books = books.Where(p => StatusesAvailableChecker.Contains(p.BookStatus.Name));
                }
                else
                {
                    books = books.Where(p => p.BookStatus.Name == "опубликованно");
                }
            }

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

        public async Task<Book> UpdateBook(Book book, IList<string> roles)
        {
            var draftStatusName = "черновик";
            var UnderConsiderationStatusName = "на рассмотрении";
            var RemovedFromPublicationStatusName = "снято с публикации";
            var PublishedStatusName = "снято с публикации";

            if (roles.Contains("Писатель"))
            {
                if (!(book.BookStatus.Name == draftStatusName || book.BookStatus.Name == UnderConsiderationStatusName))
                {
                    return null;
                }
            }
            if (roles.Contains("Проверяющий"))
            {
                if (!(book.BookStatus.Name == RemovedFromPublicationStatusName || book.BookStatus.Name == UnderConsiderationStatusName
                    || book.BookStatus.Name == PublishedStatusName))
                {
                    return null;
                }
            }

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

        public async Task<BookCover> UpdateBookCover(Guid Id, IFormFile file)
        {
            try
            {
                var book = await bookRepository.GetById(Id);
                if (book != null)
                {
                    var bookCover = await bookCoverRepository.GetById(Id);
                    if(bookCover == null)
                    {
                        await bookCoverRepository.Insert(new BookCover() { Id = book.Id, CoverImage = ImageConverter.ConvertToByteArray(file) });
                        await bookCoverRepository.Save();
                    }
                    else
                    {
                        bookCover.CoverImage = ImageConverter.ConvertToByteArray(file);
                        await bookCoverRepository.Update(bookCover);
                        await bookCoverRepository.Save();
                    }
                    return bookCover;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> GetBookCover(Guid Id)
        {
            var bookCover = await bookCoverRepository.GetById(Id);
            if (bookCover == null)
            {
                return null;
            }
            return bookCover.CoverImage;
        }

        public async Task<bool> DeleteBookCover(Guid Id)
        {
            try
            {
                await bookCoverRepository.Delete(Id);
                await bookCoverRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> IsBookCoverExist(Guid Id)
        {
            return Task.FromResult(bookCoverRepository.GetById(Id) != null);
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

        public async Task<IEnumerable<BookReview>> GetReviewsByBookId(Guid bookId)
        {
            var reviews = await bookRepository.GetReviewsByBookId(bookId);
            return reviews;
        }
    }
}
