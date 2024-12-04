namespace InveonConsoleApp.Principles.ISP.ISPSolution;

public interface ICustomerActions
{
    void PlaceOrder();
    void TrackOrder();
}

public interface IDeliveryActions
{
    void DeliverOrder();
}

public class Customer : ICustomerActions
{
    public void PlaceOrder()
    {
        Console.WriteLine("Customer placed an order.");
    }

    public void TrackOrder()
    {
        Console.WriteLine("Customer is tracking their order.");
    }
}

public class DeliveryPerson : IDeliveryActions
{
    public void DeliverOrder()
    {
        Console.WriteLine("Delivery person delivered the order.");
    }
}
