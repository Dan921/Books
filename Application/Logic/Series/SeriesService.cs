using Application.Models;
using Data.Context;
using Data.Repositories.Series;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<BookSeriesModel>> GetSeries()
        {
            var series = (await seriesRepository.GetAll()).Select(p => ModelsHelper.GetBookSeriesModel(p));
            return series;
        }

        public async Task<BookSeriesModel> GetSeriesById(Guid id)
        {
            var series = await seriesRepository.GetById(id);
            if (series != null)
            {
                return ModelsHelper.GetBookSeriesModel(series);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InsertSeries(BookSeriesModel seriesModel)
        {
            try
            {
                await seriesRepository.Insert(ModelsHelper.GetBookSeriesFromModel(seriesModel));
                await seriesRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookSeriesModel> UpdateSeries(BookSeriesModel seriesModel)
        {
            try
            {
                await seriesRepository.Update(ModelsHelper.GetBookSeriesFromModel(seriesModel));
                await seriesRepository.Save();
                return seriesModel;
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
