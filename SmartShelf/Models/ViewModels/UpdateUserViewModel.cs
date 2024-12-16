namespace SmartShelf.Models.ViewModels;

public class UpdateUserViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string Gender { get; set; }
}