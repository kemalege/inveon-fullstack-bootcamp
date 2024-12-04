namespace InveonConsoleApp.Principles.SRP.WithSRP;

public interface IMediator
{
    void Notify(object userData, string eventName);
}

public class UserMediator : IMediator
{
    private readonly NotificationService _notificationService;

    public UserMediator(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Notify(object userData, string eventName)
    {
        if (eventName == "UserCreated")
        {
            HandleUserCreated((string)userData);
        }
    }

    private void HandleUserCreated(string username)
    {
        _notificationService.SendEmail(username);
    }
}

public class UserManager
{
    private readonly IMediator _mediator;

    public UserManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void CreateUser(string username)
    {
        string userCreationMessage = $"User {username} created.";
        Console.WriteLine(userCreationMessage);
        _mediator.Notify(username, "UserCreated");
    }
}

public class NotificationService
{
    public void SendEmail(string username)
    {
        Console.WriteLine($"Email sent to {username}");
    }
}