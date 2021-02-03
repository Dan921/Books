﻿using Data.Context;
using Data.Repositories.Series;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Series
{
    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository seriesRepository;

        public SeriesService(ISeriesRepository seriesRepository)
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
            if (series != null)
            {
                return series;
            }
            else
            {
                return null;
            }
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
