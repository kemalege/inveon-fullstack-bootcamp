using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.Repositories;
using SmartShelf.Models.ViewModels;

namespace SmartShelf.Models.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(Guid id)
        {
            return await _userManager.Users
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

            var result = await _userManager.CreateAsync(user, model.Password);
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

            var result = await _userManager.UpdateAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<AssignRoleViewModel> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

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
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, new[] { "User not found." });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }


        public async Task<(bool Success, string[] Errors)> RemoveRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return (false, new[] { "User not found." });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

    }
}
