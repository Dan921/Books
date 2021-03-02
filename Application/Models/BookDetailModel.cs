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

        public DateTime PublishingDate { get; set; }

        public float Rating { get; set; }

        public int NumberOfRatings { get; set; }

        public List<Guid> GenresIds { get; set; }

        public List<Guid> TagsIds { get; set; }

        public Guid BookStatusId { get; set; }

        public Guid BookSeriesId { get; set; }

        public Guid AuthorId { get; set; }
    }
}
