using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Application.Logic;
using Microsoft.AspNetCore.Authorization;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Администратор")]
    public class BookGenresController : ControllerBase
    {
        private IBookGenresQueriesService bookGenresQueriesService;
        private IMapper mapper;

        public BookGenresController(IBookGenresQueriesService bookGenresQueriesService, IMapper mapper)
        {
            this.bookGenresQueriesService = bookGenresQueriesService;
            this.mapper = mapper;
        }

        // GET: api/BookGenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookGenreModel>>> GetBookGenres()
        {
            var genres = await bookGenresQueriesService.GetGenres();
            return Ok(genres);
        }

        // GET: api/BookGenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookGenreModel>> GetBookGenre(Guid id)
        {
            var genre = await bookGenresQueriesService.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        // PUT: api/BookGenres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookGenre([FromBody] BookGenreModel bookGenreModel)
        {
            try
            {
                var genre = mapper.Map<BookGenre>(bookGenreModel);
                var updatedGenre = await bookGenresQueriesService.UpdateGenre(genre);
                if (updatedGenre == null)
                {
                    return NotFound();
                }
                return Ok(bookGenreModel);
            }
            catch
            {
                throw;
            }
        }

        // POST: api/BookGenres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookGenreModel>> PostBookGenre([FromBody] BookGenreModel bookGenreModel)
        {
            if (bookGenreModel == null)
            {
                return NotFound();
            }
            var genre = mapper.Map<BookGenre>(bookGenreModel);
            var isGenreCreated = await bookGenresQueriesService.InsertGenre(genre);
            if (isGenreCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(bookGenreModel);
            }
        }

        // DELETE: api/BookGenres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookGenre(Guid id)
        {
            var genre = await bookGenresQueriesService.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }
            var isGenreDeleted = await bookGenresQueriesService.DeleteGenre(id);
            if (isGenreDeleted == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}
