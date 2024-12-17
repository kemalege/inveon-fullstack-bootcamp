using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.Repositories;
using SmartShelf.Models.ViewModels;

namespace SmartShelf.Models.Services
{
    public class UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        : IUserService
    {
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(Guid id)
        {
            return await userManager.Users
                .Include(u => u.UserFeature)
                .FirstOrDefaultAsync(u => u.Id == id) ?? new AppUser();
        }


        public async Task<(bool Success, string[] Errors)> CreateUserAsync(UserCreateDto model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                City = model.City,
                UserType = model.UserType
            };

            var result = await userManager.CreateAsync(user, model.Password);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Success, string[] Errors)> UpdateUserAsync(UpdateUserViewModel model)
        {
            var user = await GetUserByIdAsync(model.UserId);

            user.Email = model.Email;
            user.City = model.City ?? user.City;
            user.UserName = model.UserName;

            if (!string.IsNullOrEmpty(model.Gender))
            {
                user.UserFeature ??= new UserFeature();
                user.UserFeature.Gender = model.Gender;
            }

            var result = await userManager.UpdateAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            var result = await userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<AssignRoleViewModel> GetUserRolesAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var roles = roleManager.Roles.ToList();
            var userRoles = await userManager.GetRolesAsync(user);

            return new AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                AvailableRoles = roles.Select(r => r.Name).ToList(),
                AssignedRoles = userRoles.ToList()
            };
        }


        public async Task<(bool Success, string[] Errors)> AssignRoleAsync(Guid userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, new[] { "User not found." });
            }

            var result = await userManager.AddToRoleAsync(user, roleName);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }


        public async Task<(bool Success, string[] Errors)> RemoveRoleAsync(Guid userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, new[] { "User not found." });
            }

            var result = await userManager.RemoveFromRoleAsync(user, roleName);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

    }
}
