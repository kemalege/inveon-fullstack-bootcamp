using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartShelf.Models.Repositories;
using System.Threading.Tasks;
using SmartShelf.Models.DTOs;
using SmartShelf.Models.ViewModels;

namespace SmartShelf.Controllers;

public class RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    : Controller
{
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto model)
    {
        var role = new AppRole
        {
            Name = model.RoleName
        };

        var result = await roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role created successfully.");
        }

        return BadRequest(result.Errors);
    }
    

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] CreateRoleDto model)
    {
        var role = await roleManager.FindByNameAsync(model.RoleName);

        if (role == null)
        {
            return NotFound("Role not found.");
        }

        var result = await roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role deleted successfully.");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpGet]
    public IActionResult GetList()
    {
        var roles = roleManager.Roles.ToList();
        return Ok(roles);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto model)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            return NotFound("Role not found.");
        }

        role.Name = model.RoleName;

        var result = await roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role updated successfully.");
        }

        return BadRequest(result.Errors);
    }

}