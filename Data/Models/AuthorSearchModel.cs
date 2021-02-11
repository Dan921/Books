using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class AuthorSearchModel
    {
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
    }
}
