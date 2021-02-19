using Application.Interfaces;
using Application.Models;
using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BookSeriesQueriesService : IBookSeriesQueriesService
    {
        private readonly ISeriesRepository seriesRepository;

        public BookSeriesQueriesService(ISeriesRepository seriesRepository)
        {
            this.seriesRepository = seriesRepository;
        }

        public async Task<IEnumerable<BookSeries>> GetSeries()
        {
            var series = await seriesRepository.GetAll();
            return series;
        }

        public async Task<BookSeries> GetSeriesById(Guid id)
        {
            var series = await seriesRepository.GetById(id);
            return series;
        }

        public async Task<bool> InsertSeries(BookSeries series)
        {
            try
            {
                await seriesRepository.Insert(series);
                await seriesRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookSeries> UpdateSeries(BookSeries series)
        {
            try
            {
                await seriesRepository.Update(series);
                await seriesRepository.Save();
                return series;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteSeries(Guid Id)
        {
            try
            {
                await seriesRepository.Delete(Id);
                await seriesRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
