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

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService bookService;

        public BooksController(IBooksService bookService)
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
        public async Task<IActionResult> PutBook(Book book)
        {
            try
            {
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
        public async Task<IActionResult> PostBook(Book book)
        {
            if (book == null)
            {
                return NotFound();
            }
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
    }
}
