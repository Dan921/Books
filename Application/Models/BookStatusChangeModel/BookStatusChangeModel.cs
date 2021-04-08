using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class BookStatusChangeModel
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public BookStatus OldStatus { get; set; }

        public BookStatus NewStatus { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}
