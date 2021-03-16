using Application.Interfaces;
using Application.ViewModels;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Logic
{
    public class UserService : IUserService
    {
        UserManager<AppUser> userManager;
        private IBooksQueriesService bookService;

        public UserService(UserManager<AppUser> manager, IBooksQueriesService bookService)
        {
            this.userManager = manager;
            this.bookService = bookService;
        }

        public async Task<bool> CreateUser(CreateUserViewModel model)
        {
            try
            {
                AppUser user = new AppUser { Email = model.Email, UserName = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditUser(EditUserViewModel model)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await userManager.DeleteAsync(user);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                AppUser user = await userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    IdentityResult result =
                        await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddBookToFavorites(Guid bookId, AppUser appUser)
        {
            try
            {
                var book = await bookService.GetBookById(bookId);
                appUser.FavoriteBooks.Add(book);
                await userManager.UpdateAsync(appUser);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AppUser> FindUser(ClaimsPrincipal user)
        {
            try
            {
                return await userManager.GetUserAsync(user);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IList<string>> GetUserRoles(AppUser user)
        {
            try
            {
                return await userManager.GetRolesAsync(user);
            }
            catch
            {
                throw;
            }
        }
    }
}
