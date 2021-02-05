using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(BooksContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Author>> SearchByName(string name)
        {
            var authors = await context.Authors.Where(a => a.FullName.Contains(name)).ToListAsync();
            return authors;
        }

        public async Task<IEnumerable<Author>> SearchByDate(DateTime? birthDate, DateTime? deathDate)
        {
            if (birthDate != null && deathDate != null)
            {
                var authors = await context.Authors.Where(a => a.BirthDate > birthDate && a.DeathDate < deathDate).ToListAsync();
                return authors;
            }
            else if (birthDate != null && deathDate == null)
            {
                var authors = await context.Authors.Where(a => a.BirthDate > birthDate).ToListAsync();
                return authors;
            }
            else if (birthDate == null && deathDate != null)
            {
                var authors = await context.Authors.Where(a => a.DeathDate < deathDate).ToListAsync();
                return authors;
            }
            return await GetAll();
        }
    }
}
