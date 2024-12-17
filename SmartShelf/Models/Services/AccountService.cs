
using Microsoft.AspNetCore.Identity;
using SmartShelf.Models;
using SmartShelf.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using SmartShelf.Models.Repositories;

namespace SmartShelf.Models.Services
{
    public class AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        : IAccountService
    {
        public async Task<(bool Success, string[] Errors)> RegisterAsync(RegisterViewModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                City = model.City
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                return (true, null);
            }

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
