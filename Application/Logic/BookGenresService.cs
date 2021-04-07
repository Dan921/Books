using Application.Interfaces;
using Application.Models;
using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class BookGenresService : IBookGenresService
    {
        private readonly IGenresRepository genresRepository;

        public BookGenresService(IGenresRepository genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task<bool> DeleteGenre(Guid id)
        {
            try
            {
                await genresRepository.Delete(id);
                await genresRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookGenre> GetGenreById(Guid id)
        {
            var genre = await genresRepository.GetById(id);
            return genre;
        }

        public async Task<IEnumerable<BookGenre>> GetGenres()
        {
            var genres = await genresRepository.GetAll();
            return genres;
        }

        public async Task<bool> InsertGenre(BookGenre genre)
        {
            try
            {
                await genresRepository.Insert(genre);
                await genresRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookGenre> UpdateGenre(BookGenre genre)
        {
            try
            {
                await genresRepository.Update(genre);
                await genresRepository.Save();
                return genre;
            }
            catch
            {
                return null;
            }
        }
    }
}
