namespace InveonConsoleApp.Principles.DIP.DIPViolation;

public class EmailSender
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class Notification
{
    private readonly EmailSender _emailSender;

    public Notification()
    {
        _emailSender = new EmailSender(); // Doğrudan bağımlılık
    }

    public void Send(string message)
    {
        _emailSender.SendEmail(message);
    }
}
