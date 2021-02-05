using Application.Models;
using Data.Context;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Authors
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorsRepository authorRepository;

        public AuthorsService(IAuthorsRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthors()
        {
            var authors = (await authorRepository.GetAll()).Select(p => ModelsHelper.GetAuthorModel(p));
            return authors;
        }

        public async Task<AuthorModel> GetAuthorById(Guid id)
        {
            var author = await authorRepository.GetById(id);
            if (author != null)
            {
                return ModelsHelper.GetAuthorModel(author);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InsertAuthor(AuthorModel authorModel)
        {
            try
            {
                await authorRepository.Insert(ModelsHelper.GetAuthorFromModel(authorModel));
                await authorRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AuthorModel> UpdateAuthor(AuthorModel authorModel)
        {
            try
            {
                await authorRepository.Update(ModelsHelper.GetAuthorFromModel(authorModel));
                await authorRepository.Save();
                return authorModel;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAuthor(Guid Id)
        {
            try
            {
                await authorRepository.Delete(Id);
                await authorRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<AuthorModel>> SearchAuthorsByName(string name)
        {
            var authors = (await authorRepository.SearchByName(name)).Select(p => ModelsHelper.GetAuthorModel(p));
            return authors;
        }

        public async Task<IEnumerable<AuthorModel>> SearchAuthorsByDate(DateTime? birthDate, DateTime? deathDate)
        {
            var authors = (await authorRepository.SearchByDate(birthDate, deathDate)).Select(p => ModelsHelper.GetAuthorModel(p));
            return authors;
        }
    }
}
