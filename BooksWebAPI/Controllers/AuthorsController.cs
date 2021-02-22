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

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorsQueriesService authorsService;
        private IMapper mapper;

        public AuthorsController(IAuthorsQueriesService authorsService, IMapper mapper)
        {
            this.authorsService = authorsService;
            this.mapper = mapper;
        }

        // GET: api/Authors
        [HttpPost("List")]
        public async Task<ActionResult<AuthorsViewModel>> GetAuthorsOnPages([FromBody] AuthorSearchModel authorSearchModel, int page = 1)
        {
            int pageSize = 10;
            IQueryable<AuthorModel> authors;
            if (authorSearchModel != null)
            {
                authors = mapper.Map<IQueryable<AuthorModel>>(await authorsService.SearchBy(authorSearchModel));
                if (authors == null)
                {
                    return NotFound();
                }
            }
            else
            {
                authors = mapper.Map<IQueryable<AuthorModel>>(await authorsService.GetAuthors());
            }

            var count = await authors.CountAsync();
            var items = await authors.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AuthorsViewModel viewModel = new AuthorsViewModel
            {
                PageViewModel = pageViewModel,
                authors = items
            };
            return Ok(viewModel);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = mapper.Map<List<AuthorModel>>(await authorsService.GetAuthors());
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
