﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Администратор")]
    public class BookTagsController : ControllerBase
    {
        private IBookTagsQueriesService bookTagsQueriesService;
        private IMapper mapper;

        public BookTagsController(IBookTagsQueriesService bookTagsQueriesService, IMapper mapper)
        {
            this.bookTagsQueriesService = bookTagsQueriesService;
            this.mapper = mapper;
        }

        // GET: api/BookTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookTagModel>>> GetBookTags()
        {
            var tags = await bookTagsQueriesService.GetTags();
            return Ok(tags);
        }

        // GET: api/BookTags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookTagModel>> GetBookTag(Guid id)
        {
            var tag = await bookTagsQueriesService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }

        // PUT: api/BookTags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookTag([FromBody] BookTagModel bookTagModel)
        {
            try
            {
                var tag = mapper.Map<BookTag>(bookTagModel);
                var updatedTag = await bookTagsQueriesService.UpdateTag(tag);
                if (updatedTag == null)
                {
                    return NotFound();
                }
                return Ok(bookTagModel);
            }
            catch
            {
                throw;
            }
        }

        // POST: api/BookTags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookTagModel>> PostBookTag([FromBody] BookTagModel bookTagModel)
        {
            if (bookTagModel == null)
            {
                return NotFound();
            }
            var tag = mapper.Map<BookTag>(bookTagModel);
            var isTagCreated = await bookTagsQueriesService.InsertTag(tag);
            if (isTagCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(bookTagModel);
            }
        }

        // DELETE: api/BookTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookTag(Guid id)
        {
            var tag = await bookTagsQueriesService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            var isTagDeleted = await bookTagsQueriesService.DeleteTag(id);
            if (isTagDeleted == false)
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
