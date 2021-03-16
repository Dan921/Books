using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Login { get; set; }

        public List<Book> FavoriteBooks { get; set; }
    }
}
