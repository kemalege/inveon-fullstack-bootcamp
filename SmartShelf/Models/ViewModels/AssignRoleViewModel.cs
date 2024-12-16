namespace SmartShelf.Models.ViewModels;

public class AssignRoleViewModel
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public List<string?> AvailableRoles { get; set; } = new List<string?>();
    public List<string> AssignedRoles { get; set; } = new List<string>();
    public string? SelectedRole { get; set; }
}
