using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic.Authors
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(Guid id);
        Task<bool> InsertAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(Guid Id);
    }
}
