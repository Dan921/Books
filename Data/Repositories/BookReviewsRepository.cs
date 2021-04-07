using Data.Context;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class BookReviewsRepository : GenericRepository<BookReview>, IBookReviewsRepository
    {
        public BookReviewsRepository(LibraryContext context) : base(context)
        {
            this.context = context;
        }
    }
}
