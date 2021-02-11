using Data.Context;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }

        public Task<IEnumerable<Author>> SearchBy(AuthorSearchModel authorSearchModel)
        {
            IEnumerable<Author> authors = context.Authors;
            if (authorSearchModel.Name != null)
            {
                authors = authors.Where(a => a.FullName.Contains(authorSearchModel.Name));
            }
            if (authorSearchModel.BirthDate != null && authorSearchModel.DeathDate != null)
            {
                authors = authors.Where(a => a.BirthDate > authorSearchModel.BirthDate && a.DeathDate < authorSearchModel.DeathDate);
            }
            else if (authorSearchModel.BirthDate != null && authorSearchModel.DeathDate == null)
            {
                authors = authors.Where(a => a.BirthDate > authorSearchModel.BirthDate);
            }
            else if (authorSearchModel.BirthDate == null && authorSearchModel.DeathDate != null)
            {
                authors = authors.Where(a => a.DeathDate < authorSearchModel.DeathDate);
            }
            return Task.FromResult(authors);
        }
    }
}
