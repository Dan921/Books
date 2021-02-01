using Data.Context;
using Data.DAL;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var authors = await authorRepository.GetAll();
            return authors;
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            var author = await authorRepository.GetById(id);
            if (author != null)
            {
                return author;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InsertAuthor(Author author)
        {
            try
            {
                await authorRepository.Insert(author);
                await authorRepository.Save();
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
                await authorRepository.Update(author);
                await authorRepository.Save();
                return author;
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
    }
}
