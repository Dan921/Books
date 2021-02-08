using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task AddReview(Guid bookId, BookReview review);
        Task<IEnumerable<Book>> GetTopRated();
        Task<IEnumerable<Book>> GetTopByNumberOfRatings();
        Task<IEnumerable<Book>> SearchBy(string BookName, string authorName, string seriesName, int? year, string[] ganreNames, string[] tagNames);
    }
}
