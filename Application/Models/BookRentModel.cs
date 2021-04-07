using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class BookRentModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
