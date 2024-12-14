using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SmartShelf.Models.Repositories;
using System.Threading.Tasks;
using SmartShelf.Models.DTOs;

namespace SmartShelf.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto model)
    {
        var role = new AppRole
        {
            Name = model.RoleName
        };

        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role created successfully.");
        }

        return BadRequest(result.Errors);
    }
    

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] CreateRoleDto model)
    {
        var role = await _roleManager.FindByNameAsync(model.RoleName);

        if (role == null)
        {
            return NotFound("Role not found.");
        }

        var result = await _roleManager.DeleteAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role deleted successfully.");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpGet]
    public IActionResult GetList()
    {
        var roles = _roleManager.Roles.ToList();
        return Ok(roles);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto model)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
        {
            return NotFound("Role not found.");
        }

        role.Name = model.RoleName;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return Ok("Role updated successfully.");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpPost("Role/{roleId}/User/{userId}/Assign")]
    public async Task<IActionResult> Assign(Guid roleId, Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (user == null || role == null)
        {
            return NotFound("User or role not found.");
        }

        var result = await _userManager.AddToRoleAsync(user, role.Name);

        if (result.Succeeded)
        {
            return Ok("Role assigned successfully.");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpDelete("Role/{roleId}/User/{userId}/Remove")]
    public async Task<IActionResult> Remove(Guid roleId, Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (user == null || role == null)
        {
            return NotFound("User or role not found.");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

        if (result.Succeeded)
        {
            return Ok("Role removed successfully.");
        }

        return BadRequest(result.Errors);
    }

}