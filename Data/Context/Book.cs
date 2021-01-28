using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int PublishingYear { get; set; }
    }
}
