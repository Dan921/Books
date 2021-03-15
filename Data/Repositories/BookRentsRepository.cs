using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class BookRentsRepository : GenericRepository<BookRent>, IBookRentsRepository
    {
        public BookRentsRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
