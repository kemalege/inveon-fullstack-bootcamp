using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartShelf.Models.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.ViewModels;

namespace SmartShelf.Controllers;

[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto model)
    {
        if (string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest("Password cannot be null or empty.");
        }

        var user = new AppUser
        {
            Email = model.Email,
            UserName = model.Email,
            City = model.City,
            UserType = model.UserType
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok("User created successfully.");
        }

        return BadRequest(result.Errors);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _userManager.Users.Include(u => u.UserFeature).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var user = await _userManager.Users
            .Include(u => u.UserFeature)
            .FirstOrDefaultAsync(u => u.Id == id);

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
        var user = await _userManager.Users
            .Include(u => u.UserFeature)
            .FirstOrDefaultAsync(u => u.Id == model.UserId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.Email = model.Email;
        user.City = model.City ?? user.City;
        user.UserName = model.UserName;

        if (!string.IsNullOrEmpty(model.Gender))
        {
            if (user.UserFeature == null)
            {
                user.UserFeature = new UserFeature { Gender = model.Gender };
            }
            else
            {
                user.UserFeature.Gender = model.Gender;
            }
        }

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            TempData["Success"] = "User updated successfully";
            return RedirectToAction("List");
        }

        TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return RedirectToAction("List");
        }

        return BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> RoleManagement(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var roles = _roleManager.Roles.ToList();

        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new AssignRoleViewModel()
        {
            UserId = user.Id,
            UserName = user.UserName,
            AvailableRoles = roles.Select(r => r.Name).ToList(),
            AssignedRoles = userRoles.ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!await _roleManager.RoleExistsAsync(model.SelectedRole))
        {
            return BadRequest("Role does not exist.");
        }

        var result = await _userManager.AddToRoleAsync(user, model.SelectedRole);

        if (result.Succeeded)
        {
            TempData["Success"] = "Role assigned successfully";
        }

        TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
        return RedirectToAction("RoleManagement", new { id = model.UserId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveRole(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (string.IsNullOrEmpty(roleName))
        {
            return BadRequest("Role name is required.");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            TempData["Success"] = $"Role '{roleName}' removed successfully!";
            return RedirectToAction("RoleManagement", new { id = userId });
        }

        TempData["Error"] = string.Join(", ", result.Errors.Select(e => e.Description));
        return RedirectToAction("RoleManagement", new { id = userId });
    }
}