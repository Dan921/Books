using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class BookFilterModel
    {
        public string BookName { get; set; }
        public Guid AuthorId { get; set; }
        public string SeriesName { get; set; }
        public int? Year { get; set; }
        public Guid[] GanreIds { get; set; }
        public Guid[] TagIds { get; set; }
        public float? Rating { get; set; }
        public List<BookStatus> Statuses { get; set; }
        public bool TopRated { get; set; }
        public bool TopByPopularity { get; set; }
    }
}
