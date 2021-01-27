using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public byte[] CoverImage { get; set; }

        public int PublishingYear { get; set; }

        public Author Author { get; set; }
    }

    public class Author
    {
        public Guid Id { get; set; }
    }
}
