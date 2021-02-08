using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BookDetailModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LongDescription { get; set; }

        public byte[] CoverImage { get; set; }

        public DateTime? PublishingDate { get; set; }

        public float Rating { get; set; }

        public int NumberOfRatings { get; set; }

        public ICollection<BookGenreModel> Genres { get; set; }

        public ICollection<BookTagModel> Tags { get; set; }

        public BookStatusModel BookStatus { get; set; }

        public BookSeriesModel BookSeries { get; set; }

        public AuthorModel Author { get; set; }
    }
}
