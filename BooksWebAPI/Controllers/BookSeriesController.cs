using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Application.Logic.Series;

namespace BooksWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSeriesController : ControllerBase
    {
        private readonly ISeriesService seriesService;

        public BookSeriesController(ISeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        // GET: api/BookSeries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookSeries>>> GetBookSeries()
        {
            var series = await seriesService.GetSeries();
            return Ok(series.ToList());
        }

        // GET: api/BookSeries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookSeries>> GetBookSeriesById(Guid id)
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
        public async Task<IActionResult> PutBookSeries(BookSeries bookSeries)
        {
            try
            {
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
        public async Task<ActionResult<BookSeries>> PostBookSeries(BookSeries bookSeries)
        {
            if (bookSeries == null)
            {
                return NotFound();
            }
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
