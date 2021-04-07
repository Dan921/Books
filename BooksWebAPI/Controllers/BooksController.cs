using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Logic;
using AutoMapper;
using Data.Models;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BooksWebApi.Attributes;
using Application.Models;
using Application.Models.BookModels;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        UserManager<AppUser> userManager;
        private IBooksService bookService;
        private IMapper mapper;

        public BooksController(UserManager<AppUser> userManager, IBooksService bookService, IMapper mapper)
        {
            this.userManager = userManager;
            this.bookService = bookService;
            this.mapper = mapper;
        }

        // POST: api/Books/List/1
        [HttpPost("Page/{page}")]
        [AllowAnonymous]
        public async Task<ActionResult<BooksViewModel>> GetBooksPage([FromBody] BookFilterModel bookSearchModel, [FromRoute] int page = 1)
        {
            int pageSize = 10;
            IQueryable<Book> books;

            if (User.Identity.IsAuthenticated)
            {
                books = await bookService.GetBooks(bookSearchModel, await userManager.GetRolesAsync(await userManager.GetUserAsync(User)));
            }
            else
            {
                books = await bookService.GetBooks(bookSearchModel, null);
            }

            if (books == null)
            {
                return null;
            }

            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize);

            PageModel pageViewModel = new PageModel(count, page, pageSize);

            BooksViewModel viewModel = new BooksViewModel
            {
                PageViewModel = pageViewModel,
                books = mapper.Map<List<BookShortModel>>(items)
            };

            return Ok(viewModel);
        }

        // GET: api/Books
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<BookShortModel>>> GetAllBooks()
        {
            IQueryable<Book> books;

            if (User.Identity.IsAuthenticated)
            {
                books = await bookService.GetBooks(null, await userManager.GetRolesAsync(await userManager.GetUserAsync(User)));
            }
            else
            {
                books = await bookService.GetBooks(null, null);
            }

            if (books == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(mapper.Map<List<BookShortModel>>(books));
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BookModel>> GetBook(Guid id)
        {
            Book book;

            if (User.Identity.IsAuthenticated)
            {
                book = await bookService.GetBookById(id, await userManager.GetRolesAsync(await userManager.GetUserAsync(User)));
            }
            else
            {
                book = await bookService.GetBookById(id, null);
            }
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // PUT: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("status")]
        [AuthorizeRoles(UserRole.Admin, UserRole.Writer, UserRole.Checking)]
        public async Task<IActionResult> ChangeBookStatus(Guid BookId, BookStatus bookStatus)
        {
            try
            {
                if(await bookService.ChangeBookStatus(BookId, await userManager.GetRolesAsync(await userManager.GetUserAsync(User)), bookStatus))
                {
                    return Ok();
                }
                return Forbid();
            }
            catch
            {
                throw;
            }
        }

        // PUT: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [AuthorizeRoles(UserRole.Admin, UserRole.Writer)]
        public async Task<IActionResult> UpdateBook(BookUpdateModel bookUpdateModel)
        {
            try
            {
                var book = mapper.Map<Book>(bookUpdateModel);

                if (await bookService.UpdateBook(book))
                {
                    return Ok();
                }
                return NotFound();
            }
            catch
            {
                throw;
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AuthorizeRoles(UserRole.Admin)]
        public async Task<IActionResult> PostBook(BookModel bookDetailModel)
        {
            if (bookDetailModel == null)
            {
                return NotFound();
            }

            var book = mapper.Map<Book>(bookDetailModel);

            var isBookCreated = await bookService.InsertBook(book);
            if (isBookCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(book);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [AuthorizeRoles(UserRole.Admin)]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            if (await bookService.DeleteBook(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}/upload_cover")]
        [AuthorizeRoles(UserRole.Admin, UserRole.Writer)]
        public async Task<IActionResult> UpdateBookCover(Guid id, IFormFile file)
        {
            try
            {
                var book = await bookService.UpdateBookCover(id, file);
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok();
                }
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}/cover")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBookCover(Guid id)
        {
            var cover = await bookService.GetBookCover(id);
            if (cover == null)
            {
                return NotFound();
            }
            else
            {
                return File(cover, "image/png");
            }
        }

        [HttpDelete("{id}/cover")]
        [AuthorizeRoles(UserRole.Admin, UserRole.Writer)]
        public async Task<IActionResult> DeleteBookCover(Guid Id)
        {
            if (bookService.IsBookCoverExist(Id).Result)
            {
                var isBookDeleted = await bookService.DeleteBookCover(Id);
                if (isBookDeleted == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("{bookId}/AddReview")]
        [AllowAnonymous]
        public async Task<IActionResult> AddReview([FromRoute] Guid bookId, [FromBody] BookReviewModel bookReviewModel)
        {
            if (bookReviewModel == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                bookReviewModel.UserName = (await userManager.GetUserAsync(User)).UserName;
            }

            bookReviewModel.Time = DateTime.Now;

            var bookReview = mapper.Map<BookReview>(bookReviewModel);

            if (await bookService.AddReview(bookId, bookReview))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/Reviews")]
        public async Task<ActionResult<BookReviewModel>> GetReviews([FromQuery] Guid bookId)
        {
            var reviews = await bookService.GetReviewsByBookId(bookId);
            return Ok(reviews);
        }

        [HttpPost("{bookId}/Rent")]
        public async Task<ActionResult> ToRentBook([FromRoute] Guid bookId, [FromBody] DateTime expirationDate)
        {
            var rentModel = new BookRentModel()
            {
                UserId = (await userManager.GetUserAsync(User)).Id,
                BookId = bookId,
                ExpirationDate = expirationDate
            };

            var rent = mapper.Map<BookRent>(rentModel);
            var reviews = await bookService.ToRentBook(rent);
            return Ok(reviews);
        }

        [HttpPost("{id}/addtofavorites")]
        public async Task<ActionResult<BookReviewModel>> AddBookToFavorites([FromQuery] Guid bookId)
        {
            var user = await userManager.GetUserAsync(User);
            var res = await bookService.AddToFavorites(user.Id, bookId);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
