using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class UserFavoriteBook
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
