using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class AuthorModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? DeathDate { get; set; }

        public string BirthPlace { get; set; }

        public string Biography { get; set; }
    }
}
