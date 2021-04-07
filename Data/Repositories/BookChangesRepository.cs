using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookChangesRepository : GenericRepository<BookChange>, IBookChangesRepository
    {
        public BookChangesRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
