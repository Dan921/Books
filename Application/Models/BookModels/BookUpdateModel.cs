using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.BookModels
{
    public class BookUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LongDescription { get; set; }

        public DateTime PublishingDate { get; set; }

        public List<Guid> GenresIds { get; set; }

        public List<Guid> TagsIds { get; set; }

        public Guid BookSeriesId { get; set; }

        public Guid AuthorId { get; set; }
    }
}
