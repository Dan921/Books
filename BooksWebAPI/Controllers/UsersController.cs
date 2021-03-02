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
        UserManager<AppUser> userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { Email = model.Email, UserName = model.Email};
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
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
                AppUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    IdentityResult result =
                        await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok(model);
        }
    }
}
