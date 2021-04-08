using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class BookTagModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Guid> BooksIds { get; set; }
    }
}
