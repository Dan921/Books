using Application.Models;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Authors
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorModel>> GetAuthors();
        Task<AuthorModel> GetAuthorById(Guid id);
        Task<bool> InsertAuthor(AuthorModel author);
        Task<AuthorModel> UpdateAuthor(AuthorModel author);
        Task<bool> DeleteAuthor(Guid Id);
        Task<IEnumerable<AuthorModel>> SearchAuthorsByName(string name);
        Task<IEnumerable<AuthorModel>> SearchAuthorsByDate(DateTime? birthDate, DateTime? deathDate);
    }
}
