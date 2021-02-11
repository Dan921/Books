using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class TagsRepository : GenericRepository<BookTag>, ITagsRepository
    {
        public TagsRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
