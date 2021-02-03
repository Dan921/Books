using Data.Context;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Series
{
    public class SeriesRepository : GenericRepository<BookSeries>, ISeriesRepository
    {
        public SeriesRepository(BooksContext context) : base(context)
        {
            this.context = context;
        }
    }
}
