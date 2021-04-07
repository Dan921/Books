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
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<BookCover> BookCovers { get; set; }
        public DbSet<BookRent> BookRents { get; set; }
        public DbSet<UserFavoriteBook> UsersFavoriteBooks { get; set; }
        public DbSet<BookChange> BookChanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserFavoriteBook>().HasKey(x => new { x.UserId, x.BookId });

            builder.Entity<Book>().Property(x => x.PublishingDate).HasColumnType("date");
            builder.Entity<Book>().Property(x => x.BookStatus).HasColumnType("nvarchar(50)");
            builder.Entity<Author>().Property(x => x.BirthDate).HasColumnType("date");
            builder.Entity<Author>().Property(x => x.DeathDate).HasColumnType("date");
        }
    }
}
