using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonalAreaService
    {
        Task<IQueryable<Book>> GetRentedBooks(Guid userId);
        Task<IQueryable<Book>> GetReadedBooks(Guid userId);
        Task<IQueryable<BookReview>> GetReviews(string userName);
        Task<IQueryable<Book>> GetFavoriteBooks(Guid userId);
        Task<IQueryable<Book>> GetPublishedBooksByUserId(Guid userId);
        Task<int> GetReadersCountByBookId(Guid bookId);
        Task<IQueryable<BookStatusChange>> GetStatusChangesByBookId(Guid bookId);
        Task<IQueryable<Book>> GetVerifiedBooks(Guid userId);
    }
}
