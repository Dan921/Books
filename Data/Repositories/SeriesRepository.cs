using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class SeriesRepository : GenericRepository<BookSeries>, ISeriesRepository
    {
        public SeriesRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
