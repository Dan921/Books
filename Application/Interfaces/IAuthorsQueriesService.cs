using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Authors
{
    public interface IAuthorsQueriesService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<bool> InsertAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(Guid Id);
        Task<IEnumerable<Author>> SearchBy(string name, DateTime? birthDate, DateTime? deathDate);
    }
}
