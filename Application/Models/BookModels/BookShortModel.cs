using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.BookModels
{
    public class BookShortModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Rating { get; set; }

        public DateTime PublishingDate { get; set; }

        public string ShortDescription { get; set; }
    }
}
