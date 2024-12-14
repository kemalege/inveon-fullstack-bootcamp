using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartShelf.Models.Repositories;
using System;
using System.Threading.Tasks;
using SmartShelf.Models.DTOs;

namespace SmartShelf.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var user = await _userManager.Users.Include(u => u.UserFeature).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto model)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return NotFound("User not found.");
        }

        user.City = model.City ?? user.City;

        if (!string.IsNullOrEmpty(model.Gender) && user.UserFeature != null)
        {
            user.UserFeature.Gender = model.Gender;
        }

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Ok("User updated successfully.");
        }

        return BadRequest(result.Errors);
    }


    [HttpDelete]
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
            return Ok("User deleted successfully.");
        }

        return BadRequest(result.Errors);
    }
}