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
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Администратор")]
    public class BookSeriesController : ControllerBase
    {
        private IBookSeriesQueriesService seriesService;
        private IMapper mapper;

        public BookSeriesController(IBookSeriesQueriesService seriesService, IMapper mapper)
        {
            this.seriesService = seriesService;
            this.mapper = mapper;
        }

        // GET: api/BookSeries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSeriesModel>>> GetBookSeries()
        {
            var series = await seriesService.GetSeries();
            return Ok(series.ToList());
        }

        // GET: api/BookSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookSeriesModel>> GetBookSeriesById(Guid id)
        {
            var series = await seriesService.GetSeriesById(id);
            if (series == null)
            {
                return NotFound();
            }
            return Ok(series);
        }

        // PUT: api/BookSeries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutBookSeries(BookSeriesModel bookSeriesModel)
        {
            try
            {
                var bookSeries = mapper.Map<BookSeries>(bookSeriesModel);
                var updatedSeries = await seriesService.UpdateSeries(bookSeries);
                if (updatedSeries == null)
                {
                    return NotFound();
                }
                return Ok(bookSeries);
            }
            catch
            {
                throw;
            }
        }

        // POST: api/BookSeries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookSeriesModel>> PostBookSeries(BookSeriesModel bookSeriesModel)
        {
            if (bookSeriesModel == null)
            {
                return NotFound();
            }
            var bookSeries = mapper.Map<BookSeries>(bookSeriesModel);
            var isSeriesCreated = await seriesService.InsertSeries(bookSeries);
            if (isSeriesCreated == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok(bookSeries);
            }
        }

        // DELETE: api/BookSeries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookSeries(Guid id)
        {
            var series = await seriesService.GetSeriesById(id);
            if (series == null)
            {
                return NotFound();
            }
            var isSeriesDeleted = await seriesService.DeleteSeries(id);
            if (isSeriesDeleted == false)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
