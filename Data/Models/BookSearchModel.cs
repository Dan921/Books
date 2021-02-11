﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class BookSearchModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string SeriesName { get; set; }
        public int? Year { get; set; }
        public Guid[] GanreIds { get; set; }
        public Guid[] TagIds { get; set; }
    }
}
