﻿using System;
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

namespace BooksWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksQueriesService bookService;
        private IMapper mapper;

        public BooksController(IBooksQueriesService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<BooksViewModel>> GetBooks([FromBody] BookSearchModel bookSearchModel, int page = 1)
        {
            int pageSize = 10;
            IQueryable<BookShortModel> books;
            if (bookSearchModel != null)
            {
                books = mapper.Map<IQueryable<BookShortModel>>(await bookService.SearchBy(bookSearchModel));
                if (books == null)
                {
                    return NotFound();
                }
            }
            else
            {
                books = mapper.Map<IQueryable<BookShortModel>>(await bookService.GetBooks());
            }

            var count = await books.CountAsync();
            var items = await books.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            BooksViewModel viewModel = new BooksViewModel
            {
                PageViewModel = pageViewModel,
                books = items
            };
            return Ok(viewModel);
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
                var book = mapper.Map<Book>(bookDetailModel);

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

        [HttpDelete("{id}/cover")]
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
        public async Task<IActionResult> AddReview(Guid bookId, BookReviewModel bookReviewModel)
        {
            if (bookReviewModel == null)
            {
                return NotFound();
            }

            var bookReview = mapper.Map<BookReview>(bookReviewModel);

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
    }
}
