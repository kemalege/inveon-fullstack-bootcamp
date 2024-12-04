namespace InveonConsoleApp.Principles.LSP.LSPSolution;

public interface IVehicle
{
    void Move();
}

public interface IMotorVehicle : IVehicle
{
    void StartEngine();
}

public interface INonMotorVehicle : IVehicle
{
    void StartPedaling();
}

// Motor vehicle implementation
public class Car : IMotorVehicle
{
    public void StartEngine()
    {
        Console.WriteLine("Car engine started.");
    }

    public void Move()
    {
        Console.WriteLine("Car is moving.");
    }
}

public class Bicycle : INonMotorVehicle
{
    public void StartPedaling()
    {
        Console.WriteLine("Bicycle pedaling started.");
    }

    public void Move()
    {
        Console.WriteLine("Bicycle is moving.");
    }
}
