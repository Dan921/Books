using Application.Models;
using Application.Models.BookModels;
using AutoMapper;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Logic
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Book, BookShortModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<BookGenre, BookGenreModel>().ReverseMap();
            CreateMap<BookReview, BookReviewModel>().ReverseMap();
            CreateMap<BookSeries, BookSeriesModel>().ReverseMap();
            CreateMap<BookTag, BookTagModel>().ReverseMap();
            CreateMap<BookRent, BookRentModel>().ReverseMap();
            CreateMap<BookStatusChange, BookStatusChangeModel>().ReverseMap();
        }
    }
}
