using Application.Models;
using Application.Models.BookModels;
using AutoMapper;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Logic
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookModel>()
                .ForMember(p => p.AuthorsIds, x => x.MapFrom(src => src.Authors.Select(x => x.Id)))
                .ForMember(p => p.GenresIds, x => x.MapFrom(src => src.Genres.Select(x => x.Id)))
                .ForMember(p => p.TagsIds, x => x.MapFrom(src => src.Tags.Select(x => x.Id)))
                .ForMember(p => p.ReviewsIds, x => x.MapFrom(src => src.Reviews.Select(x => x.Id)))
                .ForMember(p => p.BookSeriesId, x => x.MapFrom(src => src.BookSeries.Id))
                .ReverseMap();

            CreateMap<Book, BookUpdateModel>()
                .ForMember(p => p.AuthorsIds, x => x.MapFrom(src => src.Authors.Select(x => x.Id)))
                .ForMember(p => p.GenresIds, x => x.MapFrom(src => src.Genres.Select(x => x.Id)))
                .ForMember(p => p.TagsIds, x => x.MapFrom(src => src.Tags.Select(x => x.Id)))
                .ForMember(p => p.BookSeriesId, x => x.MapFrom(src => src.BookSeries.Id))
                .ReverseMap();

            CreateMap<Book, BookShortModel>().ReverseMap();

            CreateMap<Author, AuthorModel>()
                .ForMember(p => p.BooksIds, x => x.MapFrom(src => src.Books.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<BookGenre, BookGenreModel>()
                .ForMember(p => p.BooksIds, x => x.MapFrom(src => src.Books.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<BookReview, BookReviewModel>()
                .ForMember(p => p.BookId, x => x.MapFrom(src => src.Book.Id))
                .ReverseMap();

            CreateMap<BookSeries, BookSeriesModel>()
                .ForMember(p => p.BooksIds, x => x.MapFrom(src => src.Books.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<BookTag, BookTagModel>()
                .ForMember(p => p.BooksIds, x => x.MapFrom(src => src.Books.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<BookRent, BookRentModel>()
                .ForMember(p => p.BookId, x => x.MapFrom(src => src.Book.Id))
                .ForMember(p => p.UserId, x => x.MapFrom(src => src.User.Id))
                .ReverseMap();

            CreateMap<BookStatusChange, BookStatusChangeModel>()
                .ForMember(p => p.BookId, x => x.MapFrom(src => src.Book.Id))
                .ReverseMap();

        }
    }
}
