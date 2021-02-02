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

        public List<BookGenre> Genres { get; set; }

        public List<BookTag> Tags { get; set; }

        public BookStatus BookStatus { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        public Guid AuthorId { get; set; }
    }
}
