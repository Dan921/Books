using Application.Interfaces;
using Application.Models;
using Application.Models.BookModels;
using Application.ViewModels;
using AutoMapper;
using BooksWebApi.Attributes;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonalAreaController : ControllerBase
    {
        UserManager<AppUser> userManager;
        private IBooksService bookService;
        IPersonalAreaService personalAreaService;
        private IMapper mapper;

        public PersonalAreaController(UserManager<AppUser> userManager, IPersonalAreaService personalAreaService,IBooksService bookService, IMapper mapper)
        {
            this.userManager = userManager;
            this.bookService = bookService;
            this.personalAreaService = personalAreaService;
            this.mapper = mapper;
        }

        [HttpGet("RentedBooks")]
        [AuthorizeRoles(UserRole.Reader, UserRole.Admin)]
        public async Task<ActionResult<List<BookShortModel>>> GetRentedBooks()
        {
            var user = await userManager.GetUserAsync(User);
            var books = await personalAreaService.GetReadedBooks(user.Id);
            if (books == null)
            {
                return NotFound();
            }

            var shortBookModels = mapper.Map<List<BookShortModel>>(books);

            return Ok(shortBookModels);
        }

        [HttpGet("ReadedBooks")]
        [AuthorizeRoles(UserRole.Reader, UserRole.Admin)]
        public async Task<ActionResult<List<BookShortModel>>> GetReadedBooks()
        {
            var user = await userManager.GetUserAsync(User);
            var books = await personalAreaService.GetReadedBooks(user.Id);
            if (books == null)
            {
                return NotFound();
            }

            var shortBookModels = mapper.Map<List<BookShortModel>>(books);

            return Ok(shortBookModels);
        }

        [HttpGet("Reviews")]
        [AuthorizeRoles(UserRole.Reader, UserRole.Admin)]
        public async Task<ActionResult<List<BookReview>>> GetReviews()
        {
            var user = await userManager.GetUserAsync(User);
            var reviews = await personalAreaService.GetReviews(user.UserName);
            if (reviews == null)
            {
                return NotFound();
            }

            //var shortBookReviewModels = mapper.Map<List<BookReviewModel>>(reviews);

            return Ok(reviews);
        }

        [HttpGet("FavoriteBooks")]
        [AuthorizeRoles(UserRole.Reader, UserRole.Admin)]
        public async Task<ActionResult<List<BookShortModel>>> GetFavoriteBooks()
        {
            var user = await userManager.GetUserAsync(User);
            var books = await personalAreaService.GetFavoriteBooks(user.Id);
            if (books == null)
            {
                return NotFound();
            }

            var shortBookModels = mapper.Map<List<BookShortModel>>(books);

            return Ok(shortBookModels);
        }

        [HttpGet("PublishedBooks")]
        [AuthorizeRoles(UserRole.Writer, UserRole.Admin)]
        public async Task<ActionResult<List<BookShortModel>>> GetPublishedBooks()
        {
            List<PublishedBookModel> publishedBookModels = new List<PublishedBookModel>();
            var user = await userManager.GetUserAsync(User);
            var books = await personalAreaService.GetPublishedBooksByUserId(user.Id);
            if (books == null)
            {
                return NotFound();
            }

            foreach (var book in books)
            {
                var publishedBookModel = new PublishedBookModel()
                {
                    Book = mapper.Map<BookShortModel>(book),
                    Reviews = mapper.Map<List<BookReviewModel>>(await bookService.GetReviewsByBookId(book.Id)),
                    StatusChanges = mapper.Map<List<BookStatusChangeModel>>(await personalAreaService.GetStatusChangesByBookId(book.Id)),
                    ReadersCount = await personalAreaService.GetReadersCountByBookId(book.Id)
            };
                publishedBookModels.Add(publishedBookModel);
            }

            return Ok(publishedBookModels);
        }
    }
}
