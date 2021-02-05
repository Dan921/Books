using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public byte[] CoverImage { get; set; }

        public DateTime? PublishingDate { get; set; }

        public float Rating { get; set; }

        public int NumberOfRatings { get; set; }

        public ICollection<BookGenre> Genres { get; set; }

        public ICollection<BookTag> Tags { get; set; }

        public BookStatus BookStatus { get; set; }

        public BookSeries BookSeries { get; set; }

        public Author Author { get; set; }
    }
}
