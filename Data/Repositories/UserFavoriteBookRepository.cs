using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class UserFavoriteBookRepository : GenericRepository<UserFavoriteBook>, IUserFavoriteBookRepository
    {
        public UserFavoriteBookRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
