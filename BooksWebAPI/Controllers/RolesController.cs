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
    [Authorize(Roles = "Администратор")]
    public class RolesController : ControllerBase
    {
        RoleManager<AppRole> roleManager;
        UserManager<AppUser> userManager;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            AppRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            AppUser user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await userManager.AddToRolesAsync(user, addedRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok();
            }

            return NotFound();
        }
    }
}
