using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Logic.Authors;
using Application.Models;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            this.authorsService = authorsService;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthors()
        {
            var authors = await authorsService.GetAuthors();
            return Ok(authors.ToList());
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorModel>> GetAuthor(Guid id)
        {
            var author = await authorsService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutAuthor(AuthorModel author)
        {
            try
            {
                var updatedAuthor = await authorsService.UpdateAuthor(author);
                if (updatedAuthor == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch
            {
                throw;
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorModel author)
        {
            if (author == null)
            {
                return NotFound();
            }
            var isAuthorCreated = await authorsService.InsertAuthor(author);
            if (isAuthorCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(author);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await authorsService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            var isAuthorDeleted = await authorsService.DeleteAuthor(id);
            if (isAuthorDeleted == false)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Authors/searchByName?name=data

        [HttpGet("searchByName")]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> SearchByName(string name)
        {
            var authors = await authorsService.SearchAuthorsByName(name);
            if (authors == null)
            {
                return NotFound();
            }
            return Ok(authors);
        }

        // GET: api/Authors/searchByDate?birthDate=data&deathDate=data

        [HttpGet("searchByDate")]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> SearchByDate(DateTime? birthDate, DateTime? deathDate)
        {
            var authors = await authorsService.SearchAuthorsByDate(birthDate, deathDate);
            if (authors == null)
            {
                return NotFound();
            }
            return Ok(authors);
        }
    }
}
