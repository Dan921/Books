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

        public Task<IOrderedQueryable<Book>> GetTopRated()
        {
            var books = context.Books.OrderBy(p => p.Rating);
            return Task.FromResult(books);
        }

        public Task<IOrderedQueryable<Book>> GetTopByNumberOfRatings()
        {
            var books = context.Books.OrderBy(p => p.NumberOfRatings);
            return Task.FromResult(books);
        }

        public Task<IEnumerable<Book>> SearchBy(BookSearchModel bookSearchModel)
        {
            IEnumerable<Book> authors = context.Books;
            if (bookSearchModel.BookName != null)
            {
                authors = authors.Where(a => a.Name.Contains(bookSearchModel.BookName));
            }
            if (bookSearchModel.AuthorName != null)
            {
                authors = authors.Where(a => a.Authors.Select(p => p.FullName).Contains(bookSearchModel.AuthorName));
            }
            if (bookSearchModel.SeriesName != null)
            {
                authors = authors.Where(a => a.BookSeries.Name.Contains(bookSearchModel.SeriesName));
            }
            if (bookSearchModel.Year != null)
            {
                authors = authors.Where(a => a.PublishingDate.Year == bookSearchModel.Year);
            }
            if (bookSearchModel.GanreIds != null)
            {
                authors = authors.Where(a => a.Genres.Select(p => p.Id).ToArray() == bookSearchModel.GanreIds);
            }
            if (bookSearchModel.TagIds != null)
            {
                authors = authors.Where(a => a.Tags.Select(p => p.Id).ToArray() == bookSearchModel.TagIds);
            }
            return Task.FromResult(authors);
        }
    }
}
