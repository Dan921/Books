using Data.Context;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddReview(Guid bookId, BookReview review)
        {
            var book = await context.Books.FindAsync(bookId);
            book.Rating = (book.Rating * book.NumberOfRatings + review.Rating) / book.NumberOfRatings + 1;
            book.NumberOfRatings += 1;
            book.Reviews.Add(review);
            await context.SaveChangesAsync();
        }

        public Task<IQueryable<BookReview>> GetReviewsByBookId(Guid bookId)
        {
            IQueryable<BookReview> reviews = context.BookReviews.Where(p => p.Book.Id == bookId);
            return Task.FromResult(reviews);
        }

        public Task<IQueryable<Book>> GetBooksUsingFilter(BookFilterModel bookFilterModel)
        {
            IQueryable<Book> authors = context.Books;
            if (bookFilterModel != null)
            {
                if (!string.IsNullOrEmpty(bookFilterModel.BookName))
                {
                    authors = authors.Where(a => a.Name.Contains(bookFilterModel.BookName));
                }
                if (bookFilterModel.AuthorId != null)
                {
                    authors = authors.Where(a => a.Authors.Select(p => p.Id).Contains(bookFilterModel.AuthorId));
                }
                if (!string.IsNullOrEmpty(bookFilterModel.SeriesName))
                {
                    authors = authors.Where(a => a.BookSeries.Name.Contains(bookFilterModel.SeriesName));
                }
                if (bookFilterModel.Year != null)
                {
                    authors = authors.Where(a => a.PublishingDate.Year == bookFilterModel.Year);
                }
                if (bookFilterModel.GanreIds != null)
                {
                    authors = authors.Where(a => a.Genres.Select(p => p.Id) == bookFilterModel.GanreIds);
                }
                if (bookFilterModel.TagIds != null)
                {
                    authors = authors.Where(a => a.Tags.Select(p => p.Id) == bookFilterModel.TagIds);
                }
                if (bookFilterModel.Rating != null)
                {
                    authors = authors.Where(a => a.Rating > bookFilterModel.Rating);
                }
                if (bookFilterModel.StatusIds != null)
                {
                    authors = authors.Where(a => bookFilterModel.StatusIds.Contains(a.BookStatus.Id));
                }
                if (bookFilterModel.TopRated == true)
                {
                    authors = authors.OrderBy(p => p.Rating);
                }
                if (bookFilterModel.TopByPopularity == true)
                {
                    authors = authors.OrderBy(p => p.NumberOfRatings);
                }
            }
            return Task.FromResult(authors);
        }
    }
}
