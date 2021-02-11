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

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksQueriesService bookService;

        public BooksController(IBooksQueriesService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookShortModel>>> GetBooks()
        {
            var books = await bookService.GetBooks();
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books.ToList());
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
        public async Task<IActionResult> PutBook(BookDetailModel bookDetailModel)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDetailModel, Book>()).CreateMapper();
                var book = mapper.Map<BookDetailModel, Book>(bookDetailModel);
                var updatedBook = await bookService.UpdateBook(book);
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
        public async Task<IActionResult> PostBook(BookDetailModel bookDetailModel)
        {
            if (bookDetailModel == null)
            {
                return NotFound();
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDetailModel, Book>()).CreateMapper();
            var book = mapper.Map<BookDetailModel, Book>(bookDetailModel);
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
                return NotFound();
            }
        }

        [HttpPut("{id}/upload_cover")]
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
        public async Task<IActionResult> GetBookCover(Guid id)
        {
            var cover = await bookService.GetBookCover(id);
            if(cover == null)
            {
                return NotFound();
            }
            else
            {
                return File(cover, "image/png");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Guid bookId, BookReviewModel bookReviewModel)
        {
            if (bookReviewModel == null)
            {
                return NotFound();
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookReviewModel, BookReview>()).CreateMapper();
            var bookReview = mapper.Map<BookReviewModel, BookReview>(bookReviewModel);
            var isReviewCreated = await bookService.AddReview(bookId, bookReview);
            if (isReviewCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(bookReviewModel);
            }
        }

        [HttpGet("TopRated")]
        public async Task<IActionResult> GetTopRated()
        {
            var books = await bookService.GetTopRated();
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books.ToList());
        }

        [HttpGet("TopByPopularity")]
        public async Task<IActionResult> GetTopByPopularity()
        {
            var books = await bookService.GetTopByNumberOfRatings();
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books.ToList());
        }

        [HttpPost("searchBy")]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> SearchBy([FromBody] BookSearchModel bookSearchModel)
        {
            var books = await bookService.SearchBy(bookSearchModel);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }
    }
}
