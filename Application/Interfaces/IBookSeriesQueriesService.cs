using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Series
{
    public interface IBookSeriesQueriesService
    {
        Task<IEnumerable<BookSeries>> GetSeries();
        Task<BookSeries> GetSeriesById(Guid id);
        Task<bool> InsertSeries(BookSeries series);
        Task<BookSeries> UpdateSeries(BookSeries series);
        Task<bool> DeleteSeries(Guid Id);
    }
}
