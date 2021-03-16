using Application.Interfaces;
using Application.ViewModels;
using Data.Context;
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
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await userService.CreateUser(model))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await userService.EditUser(model))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                if (await userService.DeleteUser(id))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await userService.ChangePassword(model))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
