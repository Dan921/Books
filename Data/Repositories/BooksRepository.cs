using Data.Context;
using Data.Interfaces;
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

        public async Task<IEnumerable<Book>> GetTopRated()
        {
            IEnumerable<Book> books = context.Books;
            await Task.Run(() =>
            {
                books = books.OrderBy(p => p.Rating);
            });
            return books;
        }

        public async Task<IEnumerable<Book>> GetTopByNumberOfRatings()
        {
            IEnumerable<Book> books = context.Books;
            await Task.Run(() =>
            {
                books = books.OrderBy(p => p.NumberOfRatings);
            });
            return books;
        }

        public async Task<IEnumerable<Book>> SearchBy(string BookName, string authorName, string seriesName, int? year, string[] ganreNames, string[] tagNames)
        {
            IEnumerable<Book> authors = context.Books;
            await Task.Run(() =>
            {
                if (BookName != null)
                {
                    authors = authors.Where(a => a.Name.Contains(BookName));
                }
                if (authorName != null)
                {
                    authors = authors.Where(a => a.Authors.Select(p => p.FullName).Contains(authorName));
                }
                if (seriesName != null)
                {
                    authors = authors.Where(a => a.BookSeries.Name.Contains(seriesName));
                }
                if (year != null)
                {
                    authors = authors.Where(a => a.PublishingDate.Year == year);
                }
                if (ganreNames != null)
                {
                    authors = authors.Where(a => a.Genres.Select(p => p.Name).ToArray() == ganreNames);
                }
                if (tagNames != null)
                {
                    authors = authors.Where(a => a.Tags.Select(p => p.Name).ToArray() == tagNames);
                }
            });
            return authors;
        }
    }
}
