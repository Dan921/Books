using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Models;
using Data.Models;
using AutoMapper;
using Application.Interfaces;
using Application.Logic;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Администратор")]
    public class AuthorsController : ControllerBase
    {
        private IAuthorsQueriesService authorsService;
        private IMapper mapper;

        public AuthorsController(IAuthorsQueriesService authorsService, IMapper mapper)
        {
            this.authorsService = authorsService;
            this.mapper = mapper;
        }

        [HttpPost("Page/{page}")]
        public async Task<ActionResult<AuthorsViewModel>> GetAuthorsPage([FromBody] AuthorFilterModel authorSearchModel, [FromQuery] int page = 1)
        {
            int pageSize = 10;
            var authors = await authorsService.GetAuthors(authorSearchModel);
            if (authors == null)
            {
                return NotFound();
            }

            var count = authors.Count();
            var items = authors.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AuthorsViewModel viewModel = new AuthorsViewModel
            {
                PageViewModel = pageViewModel,
                authors = mapper.Map<List<AuthorModel>>(items)
            };
            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = mapper.Map<List<AuthorModel>>(await authorsService.GetAuthors(null));
            if (authors == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(authors);
            }
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
                var author = mapper.Map<Author>(authorModel);
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
            var author = mapper.Map<Author>(authorModel);
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
    }
}
