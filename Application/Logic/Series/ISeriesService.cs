using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Series
{
    public interface ISeriesService
    {
        Task<IEnumerable<BookSeries>> GetSeries();
        Task<BookSeries> GetSeriesById(Guid id);
        Task<bool> InsertSeries(BookSeries author);
        Task<BookSeries> UpdateSeries(BookSeries author);
        Task<bool> DeleteSeries(Guid Id);
    }
}
