namespace InveonConsoleApp.Principles.LSP.LSPViolation;

public interface IVehicle
{
    void Start();
    void Move();
}

public class Car : IVehicle
{
    public void Start()
    {
        Console.WriteLine("Car engine started.");
    }

    public void Move()
    {
        Console.WriteLine("Car is moving.");
    }
}

public class Bicycle : IVehicle
{
    public void Start()
    {
        throw new NotImplementedException("Bicycle cannot start an engine.");
    }

    public void Move()
    {
        Console.WriteLine("Bicycle is pedaled forward.");
    }
}
