using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.ViewModels;
using SmartShelf.Models.Services;

namespace SmartShelf.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto model)
        {
            var result = await userService.CreateUserAsync(model);
            if (result.Success)
            {
                return Ok("User created successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var model = new UpdateUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Gender = user.UserFeature?.Gender
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewModel model)
        {
            var result = await userService.UpdateUserAsync(model);

            if (result.Success)
            {
                TempData["Success"] = "User updated successfully.";
                return RedirectToAction("List");
            }

            TempData["Error"] = string.Join(", ", result.Errors);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await userService.DeleteUserAsync(id);
            if (success)
            {
                return RedirectToAction("List");
            }

            return BadRequest("Error deleting user.");
        }

        [HttpGet]
        public async Task<IActionResult> RoleManagement(Guid id)
        {
            var model = await userService.GetUserRolesAsync(id);
            if (model == null)
            {
                return NotFound("User not found.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            var result = await userService.AssignRoleAsync(model.UserId, model.SelectedRole);
            if (result.Success)
            {
                TempData["Success"] = "Role assigned successfully.";
            }
            else
            {
                TempData["Error"] = string.Join(", ", result.Errors);
            }

            return RedirectToAction("RoleManagement", new { id = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(Guid userId, string roleName)
        {
            var result = await userService.RemoveRoleAsync(userId, roleName);
            if (result.Success)
            {
                TempData["Success"] = $"Role '{roleName}' removed successfully!";
            }
            else
            {
                TempData["Error"] = string.Join(", ", result.Errors);
            }

            return RedirectToAction("RoleManagement", new { id = userId });
        }
    }
}
