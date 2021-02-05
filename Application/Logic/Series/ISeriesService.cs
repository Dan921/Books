using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Series
{
    public interface ISeriesService
    {
        Task<IEnumerable<BookSeriesModel>> GetSeries();
        Task<BookSeriesModel> GetSeriesById(Guid id);
        Task<bool> InsertSeries(BookSeriesModel seriesModel);
        Task<BookSeriesModel> UpdateSeries(BookSeriesModel seriesModel);
        Task<bool> DeleteSeries(Guid Id);
    }
}
