using Application.Models;
using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorsQueriesService
    {
        Task<IEnumerable<Author>> GetAuthors(AuthorFilterModel authorSearchModel);
        Task<Author> GetAuthorById(Guid id);
        Task<bool> InsertAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(Guid Id);
    }
}
