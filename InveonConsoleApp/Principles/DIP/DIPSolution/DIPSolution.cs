namespace InveonConsoleApp.Principles.DIP.DIPSolution;

public interface IMessageSender
{
    void SendMessage(string message);
}

public class EmailSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Email sent: {message}");
    }
}

public class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"SMS sent: {message}");
    }
}

public class Notification
{
    private readonly IMessageSender _messageSender;

    public Notification(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public void Send(string message)
    {
        _messageSender.SendMessage(message);
    }
}