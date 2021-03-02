using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class LibraryContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<BookSeries> BookSeries { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<BookCover> BookCovers { get; set; }

    }
}
