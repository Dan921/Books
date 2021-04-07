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

        public List<Guid> GenresIds { get; set; }

        public List<Guid> TagsIds { get; set; }

        public List<Guid> ReviewsIds { get; set; }

        public List<Guid> AuthorsIds { get; set; }

        public BookStatus BookStatus { get; set; }
    }
}
