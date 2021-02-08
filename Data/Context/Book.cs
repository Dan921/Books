using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Context
{
    public class Book
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        public byte[] CoverImage { get; set; }

        public DateTime PublishingDate { get; set; }

        public float Rating { get; set; }

        public int NumberOfRatings { get; set; }

        public ICollection<BookGenre> Genres { get; set; }

        public ICollection<BookTag> Tags { get; set; }

        public ICollection<BookReview> Reviews { get; set; }

        public ICollection<Author> Authors { get; set; }

        public BookStatusModel BookStatus { get; set; }

        public BookSeries BookSeries { get; set; }
    }
}
