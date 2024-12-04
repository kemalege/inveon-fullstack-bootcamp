namespace InveonConsoleApp.Principles.SRP.SRPViolation;

public class UserManager
{
    public void CreateUser(string username)
    {
        Console.WriteLine($"User {username} created.");
        SendEmail(username);
    }

    private void SendEmail(string username)
    {
        Console.WriteLine($"Email sent to {username}");
    }
}