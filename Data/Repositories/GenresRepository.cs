using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class GenresRepository : GenericRepository<BookGenre>, IGenresRepository
    {
        public GenresRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
