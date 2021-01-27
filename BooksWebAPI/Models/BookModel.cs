using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebAPI.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public byte[] CoverImage { get; set; }
        public int PublishingYear { get; set; }
        public string Author { get; set; }
    }
}
