using SmartShelf.Models.Repositories;

namespace SmartShelf.Models.DTOs;

public class UserCreateDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string City { get; set; } = default!;
    public UserType UserType { get; set; }
}
