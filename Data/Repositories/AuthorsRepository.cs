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

        public Task<IEnumerable<Author>> GetAuthorsUsingFilter(AuthorFilterModel authorFilterModel)
        {
            IEnumerable<Author> authors = context.Authors;
            if(authorFilterModel != null)
            {
                if (!string.IsNullOrEmpty(authorFilterModel.Name))
                {
                    authors = authors.Where(a => a.FullName.Contains(authorFilterModel.Name));
                }
                if (authorFilterModel.BirthDate != null && authorFilterModel.DeathDate != null)
                {
                    authors = authors.Where(a => a.BirthDate > authorFilterModel.BirthDate && a.DeathDate < authorFilterModel.DeathDate);
                }
                else if (authorFilterModel.BirthDate != null && authorFilterModel.DeathDate == null)
                {
                    authors = authors.Where(a => a.BirthDate > authorFilterModel.BirthDate);
                }
                else if (authorFilterModel.BirthDate == null && authorFilterModel.DeathDate != null)
                {
                    authors = authors.Where(a => a.DeathDate < authorFilterModel.DeathDate);
                }
            }
            return Task.FromResult(authors);
        }
    }
}
