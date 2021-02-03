using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class BookSeries
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
