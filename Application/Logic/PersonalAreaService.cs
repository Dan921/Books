using Data.Context;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class PersonalAreaService
    {
        private readonly IBooksRepository bookRepository;
        private readonly IBookCoverRepository bookCoverRepository;
        private readonly IBookRentsRepository bookRentsRepository;
        private readonly IUserFavoriteBookRepository userFavoriteBookRepository;
        private readonly IBookChangesRepository bookChangesRepository;
        private readonly IBookReviewsRepository bookReviewsRepository;
        private readonly UserManager<AppUser> userManager;

        public PersonalAreaService(IBooksRepository bookRepository,
            IBookCoverRepository bookCoverRepository,
            IBookRentsRepository bookRentsRepository,
            IUserFavoriteBookRepository userFavoriteBookRepository,
            IBookChangesRepository bookChangesRepository,
            IBookReviewsRepository bookReviewsRepository,
            UserManager<AppUser> userManager)
        {
            this.bookRepository = bookRepository;
            this.bookCoverRepository = bookCoverRepository;
            this.bookRentsRepository = bookRentsRepository;
            this.userFavoriteBookRepository = userFavoriteBookRepository;
            this.bookChangesRepository = bookChangesRepository;
            this.bookReviewsRepository = bookReviewsRepository;
            this.userManager = userManager;
        }

        public async Task<IQueryable<Book>> GetRentedBooks(Guid userId)
        {
            try
            {
                var bookRents = await bookRentsRepository.GetAll();
                var books = bookRents.Where(p => p.User.Id == userId).Select(p => p.Book);
                return books;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Book>> GetReadedBooks(Guid userId)
        {
            try
            {
                var bookRents = await bookRentsRepository.GetAll();
                var books = bookRents.Where(p => p.User.Id == userId && p.ExpirationDate < DateTime.Now).Select(p => p.Book);
                return books;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<BookReview>> GetReviews(string userName)
        {
            try
            {
                var allReviews = await bookReviewsRepository.GetAll();
                var reviews = allReviews.Where(p => p.UserName == userName);
                return reviews;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IQueryable<Book>> GetFavoriteBooks(Guid userId)
        {
            try
            {
                var favoriteBooks = await userFavoriteBookRepository.GetAll();
                var booksIds = favoriteBooks.Where(p => p.UserId == userId).Select(p => p.BookId);
                var allbooks = await bookRepository.GetAll();
                var books = allbooks.Where(p => booksIds.Contains(p.Id));
                return books;
            }
            catch
            {
                return null;
            }
        }
    }
}
