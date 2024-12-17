using SmartShelf.Models.ViewModels;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.Repositories;

namespace SmartShelf.Models.Services
{
    public interface IUserService
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(Guid id);
        Task<(bool Success, string[] Errors)> CreateUserAsync(UserCreateDto model);
        Task<(bool Success, string[] Errors)> UpdateUserAsync(UpdateUserViewModel model);
        Task<bool> DeleteUserAsync(Guid id);
        Task<AssignRoleViewModel> GetUserRolesAsync(Guid userId);
        Task<(bool Success, string[] Errors)> AssignRoleAsync(Guid userId, string roleName);
        Task<(bool Success, string[] Errors)> RemoveRoleAsync(Guid userId, string roleName);
    }
}