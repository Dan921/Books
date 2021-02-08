using Data.Context;
using Data.Interfaces;
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

        public async Task<IEnumerable<Author>> SearchBy(string name, DateTime? birthDate, DateTime? deathDate)
        {
            IEnumerable<Author> authors = context.Authors;
            await Task.Run(() =>
            {
                if (name != null)
                {
                    authors = authors.Where(a => a.FullName.Contains(name));
                }
                if (birthDate != null && deathDate != null)
                {
                    authors = authors.Where(a => a.BirthDate > birthDate && a.DeathDate < deathDate);
                }
                else if (birthDate != null && deathDate == null)
                {
                    authors = authors.Where(a => a.BirthDate > birthDate);
                }
                else if (birthDate == null && deathDate != null)
                {
                    authors = authors.Where(a => a.DeathDate < deathDate);
                }
            });
            return authors;
        }
    }
}
