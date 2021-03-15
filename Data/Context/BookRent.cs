using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Context
{
    public class BookRent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public AppUser User { get; set; }
        public Book Book { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
