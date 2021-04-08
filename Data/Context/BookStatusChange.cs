using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Context
{
    public class BookStatusChange
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Book Book { get; set; }

        public BookStatus OldStatus { get; set; }

        public BookStatus NewStatus { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
