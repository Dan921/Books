using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ModelsHelper
    {
        public static BookShortModel GetBookShortModel(Book book)
        {
            var bookShortModel = new BookShortModel()
            {
                Id = book.Id,
                Name = book.Name,
                ShortDescription = book.ShortDescription
            };
            return bookShortModel;
        }

        public static BookModel GetBookModel(Book book)
        {
            var bookModel = new BookModel()
            {
                Id = book.Id,
                Name = book.Name,
                ShortDescription = book.ShortDescription,
                LongDescription = book.LongDescription,
                CoverImage = book.CoverImage,
                PublishingDate = book.PublishingDate,
                Rating = book.Rating,
                NumberOfRatings = book.NumberOfRatings,
                Genres = book.Genres,
                Tags = book.Tags,
                BookStatus = book.BookStatus,
                BookSeries = book.BookSeries,
                Author = book.Author,
            };
            return bookModel;
        }

        public static AuthorModel GetAuthorModel(Author author)
        {
            var authorModel = new AuthorModel()
            {
                Id = author.Id,
                FullName = author.FullName,
                BirthDate = author.BirthDate,
                DeathDate = author.DeathDate,
                BirthPlace = author.BirthPlace,
                Biography = author.Biography,
                Books = author.Books,
            };
            return authorModel;
        }

        public static BookSeriesModel GetBookSeriesModel(BookSeries series)
        {
            var seriesModel = new BookSeriesModel()
            {
                Id = series.Id,
                Name = series.Name,
                Books = series.Books
            };
            return seriesModel;
        }

        public static BookGenreModel GetBookGenreModel(BookGenre genre)
        {
            var genreModel = new BookGenreModel()
            {
                Id = genre.Id,
                Name = genre.Name,
                Books = genre.Books
            };
            return genreModel;
        }

        public static BookTagModel GetBookTagModel(BookTag tag)
        {
            var tagModel = new BookTagModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                Books = tag.Books
            };
            return tagModel;
        }

        public static Book GetBookFromModel(BookModel bookModel)
        {
            var book = new Book()
            {
                Id = bookModel.Id,
                Name = bookModel.Name,
                ShortDescription = bookModel.ShortDescription,
                LongDescription = bookModel.LongDescription,
                CoverImage = bookModel.CoverImage,
                PublishingDate = bookModel.PublishingDate,
                Rating = bookModel.Rating,
                NumberOfRatings = bookModel.NumberOfRatings,
                Genres = bookModel.Genres,
                Tags = bookModel.Tags,
                BookStatus = bookModel.BookStatus,
                BookSeries = bookModel.BookSeries,
                Author = bookModel.Author,
            };
            return book;
        }

        public static Author GetAuthorFromModel(AuthorModel authorModel)
        {
            var author = new Author()
            {
                Id = authorModel.Id,
                FullName = authorModel.FullName,
                BirthDate = authorModel.BirthDate,
                DeathDate = authorModel.DeathDate,
                BirthPlace = authorModel.BirthPlace,
                Biography = authorModel.Biography,
                Books = authorModel.Books,
            };
            return author;
        }

        public static BookSeries GetBookSeriesFromModel(BookSeriesModel seriesModel)
        {
            var series = new BookSeries()
            {
                Id = seriesModel.Id,
                Name = seriesModel.Name,
                Books = seriesModel.Books
            };
            return series;
        }

        public static BookGenre GetBookGenreFromModel(BookGenreModel genreModel)
        {
            var genre = new BookGenre()
            {
                Id = genreModel.Id,
                Name = genreModel.Name,
                Books = genreModel.Books
            };
            return genre;
        }

        public static BookTag GetBookTagFromModel(BookTagModel tagModel)
        {
            var tag = new BookTag()
            {
                Id = tagModel.Id,
                Name = tagModel.Name,
                Books = tagModel.Books
            };
            return tag;
        }
    }
}
