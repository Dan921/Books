using Application.ViewModels;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(CreateUserViewModel model);
        Task<bool> EditUser(EditUserViewModel model);
        Task<bool> DeleteUser(string id);
        Task<bool> ChangePassword(ChangePasswordViewModel model);
        Task<bool> AddBookToFavorites(Guid bookId, AppUser appUser);
        Task<AppUser> FindUser(ClaimsPrincipal user);
        Task<IList<string>> GetUserRoles(AppUser user);
    }
}
