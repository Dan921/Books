using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(BooksContext context) : base(context)
        {
            this.context = context;
        }
    }
}
