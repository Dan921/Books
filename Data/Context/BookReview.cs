using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Context
{
    public class BookReview
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }

        public float Rating { get; set; }

        public Book Book { get; set; }
    }
}
