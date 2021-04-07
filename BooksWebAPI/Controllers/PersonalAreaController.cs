using Application.Interfaces;
using AutoMapper;
using BooksWebApi.Attributes;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonalAreaController : ControllerBase
    {
        UserManager<AppUser> userManager;
        private IBooksService bookService;
        private IMapper mapper;

        public PersonalAreaController(UserManager<AppUser> userManager, IBooksService bookService, IMapper mapper)
        {
            this.userManager = userManager;
            this.bookService = bookService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AuthorizeRoles(UserRole.Reader)]
        public async Task<ActionResult<BooksViewModel>> GetAllBooks()
        {
            IQueryable<Book> books;

            if (User.Identity.IsAuthenticated)
            {
                books = await bookService.GetBooks(null, await userManager.GetRolesAsync(await userManager.GetUserAsync(User)));
            }
            else
            {
                books = await bookService.GetBooks(null, null);
            }

            if (books == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }
        }
    }
}
