using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class BookReviewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public DateTime Time { get; set; }

        public string Text { get; set; }

        public float Rating { get; set; }

        public Guid BookId { get; set; }
}
}
