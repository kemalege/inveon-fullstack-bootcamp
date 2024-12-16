using SmartShelf.Models.ViewModels;

namespace SmartShelf.Models.Services
{
    public interface IAccountService
    {
        Task<(bool Success, string[] Errors)> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}