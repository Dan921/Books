using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        public BooksRepository(BooksContext context) : base(context)
        {
            this.context = context;
        }
    }
}
