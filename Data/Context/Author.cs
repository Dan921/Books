using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Context
{
    public class Author
    {
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string BirthPlace { get; set; }

        public string Biography { get; set; }

        public List<Book> Books { get; set; }
    }
}
