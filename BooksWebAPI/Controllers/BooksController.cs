using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Logic;
using Application.Models;
using AutoMapper;
using Data.Models;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        IUserService userService;
        private IBooksQueriesService bookService;
        private IMapper mapper;

        public BooksController(IUserService userService, IBooksQueriesService bookService, IMapper mapper)
        {
            this.userService = userService;
            this.bookService = bookService;
            this.mapper = mapper;
        }

        // POST: api/Books/List/1
        [HttpPost("Page/{page}")]
        [AllowAnonymous]
        public async Task<ActionResult<BooksViewModel>> GetBooksPage([FromBody] BookFilterModel bookSearchModel, [FromQuery] int page = 1)
        {
            int pageSize = 10;
            var books = await bookService.GetBooks(bookSearchModel, await userService.GetUserRoles(await userService.FindUser(User)));
            if (books == null)
            {
                return null;
            }

            var count = books.Count();
            var items = books.Skip((page - 1) * pageSize).Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

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
        public async Task<ActionResult<BooksViewModel>> GetAllBooks()
        {
            var books = await bookService.GetBooks(null, await userService.GetUserRoles(await userService.FindUser(User)));
            if (books == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailModel>> GetBook(Guid id)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // PUT: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Администратор, Писатель, Проверяющий")]
        public async Task<IActionResult> PutBook(BookDetailModel bookDetailModel)
        {
            try
            {
                var book = mapper.Map<Book>(bookDetailModel);

                var updatedBook = await bookService.UpdateBook(book, await userService.GetUserRoles(await userService.FindUser(User)));
                if (updatedBook == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch
            {
                throw;
            }
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> PostBook(BookDetailModel bookDetailModel)
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
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            var isBookDeleted = await bookService.DeleteBook(id);
            if (isBookDeleted == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut("{id}/upload_cover")]
        [Authorize(Roles = "Администратор, Писатель, Проверяющий")]
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
        [Authorize(Roles = "Администратор")]
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

        [HttpPost("AddReview")]
        [AllowAnonymous]
        public async Task<IActionResult> AddReview(Guid bookId, BookReviewModel bookReviewModel)
        {
            if (bookReviewModel == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                bookReviewModel.UserName = (await userService.FindUser(User)).UserName;
            }

            bookReviewModel.Time = DateTime.Now;

            var bookReview = mapper.Map<BookReview>(bookReviewModel);

            var isReviewCreated = await bookService.AddReview(bookId, bookReview);
            if (isReviewCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("{id}/Reviews")]
        public async Task<ActionResult<BookReviewModel>> GetReviews([FromQuery] Guid bookId)
        {
            var reviews = await bookService.GetReviewsByBookId(bookId);
            return Ok(reviews);
        }

        [HttpPost("{id}/Rent")]
        public async Task<ActionResult<BookReviewModel>> ToRentBook([FromQuery] Guid bookId, [FromBody] DateTime expirationDate)
        {
            var rent = new BookRent()
            {
                User = await userService.FindUser(User),
                Book = await bookService.GetBookById(bookId),
                ExpirationDate = expirationDate
            };
            var reviews = await bookService.ToRentBook(rent);
            return Ok(reviews);
        }

        [Authorize(Roles = "Писатель, Администратор")]
        [HttpPost("{id}/addеtofavorites")]
        public async Task<ActionResult<BookReviewModel>> AddBookToFavorites([FromQuery] Guid bookId)
        {
            var user = await userService.FindUser(User);
            var res = await userService.AddBookToFavorites(bookId, user);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
