using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task AddReview(Guid bookId, BookReview review);
        Task<IQueryable<Book>> GetBooksUsingFilter(BookFilterModel bookFilterModel);
        Task<IQueryable<BookReview>> GetReviewsByBookId(Guid bookId);
    }
}
