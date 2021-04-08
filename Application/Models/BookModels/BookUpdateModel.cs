using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.BookModels
{
    public class BookUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public DateTime PublishingDate { get; set; }

        public Guid BookSeriesId { get; set; }

        public IEnumerable<Guid> GenresIds { get; set; }

        public IEnumerable<Guid> TagsIds { get; set; }

        public IEnumerable<Guid> AuthorsIds { get; set; }
    }
}
