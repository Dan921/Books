using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.BookGenres
{
    public interface IBookGenresQueriesService
    {
        Task<IEnumerable<BookGenre>> GetGenres();
        Task<BookGenre> GetGenreById(Guid id);
        Task<bool> InsertGenre(BookGenre genre);
        Task<BookGenre> UpdateGenre(BookGenre genre);
        Task<bool> DeleteGenre(Guid Id);
    }
}
