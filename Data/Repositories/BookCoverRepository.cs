using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class BookCoverRepository : GenericRepository<BookCover>, IBookCoverRepository
    {
        public BookCoverRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
