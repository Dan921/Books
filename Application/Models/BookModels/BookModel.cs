using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.BookModels
{
    public class BookModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LongDescription { get; set; }

        public DateTime PublishingDate { get; set; }

        public float Rating { get; set; }

        public int NumberOfRatings { get; set; }

        public Guid BookSeriesId { get; set; }

        public IEnumerable<Guid> GenresIds { get; set; }

        public IEnumerable<Guid> TagsIds { get; set; }

        public IEnumerable<Guid> ReviewsIds { get; set; }

        public IEnumerable<Guid> AuthorsIds { get; set; }

        public BookStatus BookStatus { get; set; }
    }
}
