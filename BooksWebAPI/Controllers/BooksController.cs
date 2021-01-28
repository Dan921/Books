using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Repositories;
using Application.Logic;
using Application.Models;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookShortModel>>> GetBooks()
        {
            var books = await bookService.GetBooks();
            return books.ToList();
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

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(Guid id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                await bookService.UpdateBook(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await bookService.BookExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if(book == null)
            {
                return NotFound();
            }

            try
            {
                await bookService.InsertBook(book);
            }
            catch
            {
                throw;
            }

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                await bookService.DeleteBook(id);
            }
            catch
            {
                throw;
            }

            return NoContent();
        }
    }
}
