using Application.Interfaces;
using Application.Models;
using Data.Context;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class AuthorsQueriesService : IAuthorsQueriesService
    {
        private readonly IAuthorsRepository authorsRepository;

        public AuthorsQueriesService(IAuthorsRepository authorsRepository)
        {
            this.authorsRepository = authorsRepository;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var authors = await authorsRepository.GetAll();
            return authors;
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            var author = await authorsRepository.GetById(id);
            return author;
        }

        public async Task<bool> InsertAuthor(Author author)
        {
            try
            {
                await authorsRepository.Insert(author);
                await authorsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            try
            {
                await authorsRepository.Update(author);
                await authorsRepository.Save();
                return author;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAuthor(Guid id)
        {
            try
            {
                await authorsRepository.Delete(id);
                await authorsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Author>> SearchBy(AuthorFilterModel authorSearchModel)
        {
            var authors = await authorsRepository.SearchBy(authorSearchModel);
            return authors;
        }
    }
}
