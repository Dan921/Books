using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Models;
using AutoMapper;
using Data.Models;
using Application.Interfaces;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsQueriesService authorsService;

        public AuthorsController(IAuthorsQueriesService authorsService)
        {
            this.authorsService = authorsService;                                                                                       
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> GetAuthors()
        {
            var authors = await authorsService.GetAuthors();
            return Ok(authors);
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
        public async Task<IActionResult> PutAuthor(AuthorModel authorModel)
        {
            try
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>()).CreateMapper();
                var author = mapper.Map<AuthorModel, Author>(authorModel);
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
        public async Task<ActionResult<AuthorModel>> PostAuthor(AuthorModel authorModel)
        {
            if (authorModel == null)
            {
                return NotFound();
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorModel, Author>()).CreateMapper();
            var author = mapper.Map<AuthorModel, Author>(authorModel);
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
                return Ok();
            }
        }

        [HttpPost("searchBy")]
        public async Task<ActionResult<IEnumerable<AuthorModel>>> SearchBy([FromBody] AuthorSearchModel authorSearchModel)
        {
            var authors = await authorsService.SearchBy(authorSearchModel);
            if (authors == null)
            {
                return NotFound();
            }
            return Ok(authors);
        }
    }
}
