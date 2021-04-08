using Application.Interfaces;
using Application.Models;
using Data.Context;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
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
        private List<BookStatus> StatusesAvailableForChecking = new List<BookStatus>() { BookStatus.UnderConsideration, BookStatus.Published, BookStatus.Unpublished };

        private readonly IBooksRepository bookRepository;
        private readonly IBookCoverRepository bookCoverRepository;
        private readonly IBookRentsRepository bookRentsRepository;
        private readonly IUserFavoriteBookRepository userFavoriteBookRepository;
        private readonly IBookChangesRepository bookChangesRepository;
        private readonly IBookStatusChangeRepository bookStatusChangeRepository;

        public BooksService(IBooksRepository bookRepository, 
            IBookCoverRepository bookCoverRepository, 
            IBookRentsRepository bookRentsRepository,
            IUserFavoriteBookRepository userFavoriteBookRepository,
            IBookChangesRepository bookChangesRepository,
            IBookStatusChangeRepository bookStatusChangeRepository)
        {
            this.bookStatusChangeRepository = bookStatusChangeRepository;
            this.bookRepository = bookRepository;
            this.bookCoverRepository = bookCoverRepository;
            this.bookRentsRepository = bookRentsRepository;
            this.userFavoriteBookRepository = userFavoriteBookRepository;
            this.bookChangesRepository = bookChangesRepository;
        }

        public async Task<Book> GetBookById(Guid id, IList<string> roles)
        {
            bool haveRights = false;

            var book = await bookRepository.GetById(id);

            if(book != null)
            {
                if (roles == null || roles.Contains(nameof(UserRole.Checking)))
                {
                    if (book.BookStatus == BookStatus.Published)
                    {
                        haveRights = true;
                    }
                }
                if (roles.Contains(nameof(UserRole.Checking)))
                {
                    if (StatusesAvailableForChecking.Contains(book.BookStatus))
                    {
                        haveRights = true;
                    }
                }
                if (roles.Contains(nameof(UserRole.Admin)) || roles.Contains(nameof(UserRole.Writer)))
                {
                    haveRights = true;
                }
                if (haveRights)
                {
                    return book;
                }
            }
            return null;
        }

        public async Task<IQueryable<Book>> GetBooks(BookFilterModel bookSearchModel, IList<string> roles)
        {
            var filteredBooks = await bookRepository.GetBooksUsingFilter(bookSearchModel);
            IQueryable<Book> books = null;

            if(roles == null || roles.Contains(nameof(UserRole.Checking)))
            {
                books = books.Union(filteredBooks.Where(p => p.BookStatus == BookStatus.Published));
            }
            if (roles.Contains(nameof(UserRole.Checking)))
            {
                books = books.Union(filteredBooks.Where(p => StatusesAvailableForChecking.Contains(p.BookStatus)));
            }
            if (roles.Contains(nameof(UserRole.Admin)) || roles.Contains(nameof(UserRole.Writer)))
            {
                books = books.Union(filteredBooks);
            }
            return books;
        }

        public async Task<bool> InsertBook(Book book, Guid userId)
        {
            try
            {
                book.PublishedBy = userId;
                await bookRepository.Insert(book);
                await bookRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangeBookStatus(Guid bookId, IList<string> roles, BookStatus bookStatus)
        {
            var haveRights = false;
            if (roles.Contains(nameof(UserRole.Admin)))
            {
                haveRights = true;
            }
            if (roles.Contains(nameof(UserRole.Writer)))
            {
                if (bookStatus == BookStatus.Draft || bookStatus == BookStatus.UnderConsideration)
                {
                    haveRights = true;
                }
            }
            if (roles.Contains(nameof(UserRole.Checking)))
            {
                if (bookStatus == BookStatus.Unpublished || bookStatus == BookStatus.UnderConsideration
                    || bookStatus == BookStatus.Published)
                {
                    haveRights = true;
                }
            }
            if (haveRights)
            {
                var book = await bookRepository.GetById(bookId);
                var bookStatusChange = new BookStatusChange()
                {
                    Book = book,
                    OldStatus = book.BookStatus,
                    NewStatus = bookStatus,
                    ChangeDate = DateTime.Now
                };

                await bookStatusChangeRepository.Insert(bookStatusChange);
                await bookStatusChangeRepository.Save();

                book.BookStatus = bookStatus;
                await bookRepository.Update(book);
                await bookRepository.Save();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateBook(Book book)
        {
            try
            {
                if (book.BookStatus == BookStatus.Draft || book.BookStatus == BookStatus.UnderConsideration)
                {
                    await bookRepository.Update(book);
                    await bookRepository.Save();
                    return true;
                }
                if (book.BookStatus == BookStatus.Published)
                {
                    var newBook = book;
                    newBook.BookStatus = BookStatus.UnderConsideration;
                    await bookRepository.Insert(book);
                    await bookRepository.Save();

                    var boolChange = new BookChange()
                    {
                        Book = book,
                        ChangedBook = newBook
                    };
                    await bookChangesRepository.Insert(boolChange);
                    await bookChangesRepository.Save();

                    return true;
                }
                if (book.BookStatus == BookStatus.Unpublished)
                {
                    var newBook = book;
                    newBook.BookStatus = BookStatus.UnderConsideration;
                    await bookRepository.Insert(book);
                    await bookRepository.Save();

                    var boolChange = new BookChange()
                    {
                        Book = newBook,
                    };
                    await bookChangesRepository.Insert(boolChange);
                    await bookChangesRepository.Save();

                    return true;
                }
                return false;
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

        public async Task<bool> ToRentBook(BookRent bookRent)
        {
            try
            {
                await bookRentsRepository.Insert(bookRent);
                await bookRentsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddToFavorites(Guid userId, Guid bookId)
        {
            if(await bookRepository.GetById(bookId) != null)
            {
                try
                {
                    var userFavoriteBook = new UserFavoriteBook()
                    {
                        UserId = userId,
                        BookId = bookId
                    };
                    await userFavoriteBookRepository.Insert(userFavoriteBook);
                    await userFavoriteBookRepository.Save();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
