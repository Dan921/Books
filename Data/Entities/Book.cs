using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public byte[] CoverImage { get; set; }
        public int PublishingYear { get; set; }
        
        //[ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }        
        //public Guid AuthorId { get; set; }
    }

    public class Author 
    {
        public Guid Id { get; set; }
    }

}
