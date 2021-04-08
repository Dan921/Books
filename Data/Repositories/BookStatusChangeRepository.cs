using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class BookStatusChangeRepository : GenericRepository<BookStatusChange>, IBookStatusChangeRepository
    {
        public BookStatusChangeRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
