using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class BookGenreModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Book> BooksIds { get; set; }
    }
}
