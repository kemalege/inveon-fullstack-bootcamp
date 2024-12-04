namespace InveonConsoleApp.Principles.ISP.ISPViolation;

public interface IUser
{
    void PlaceOrder();
    void TrackOrder();
    void DeliverOrder();
}

public class Customer : IUser
{
    public void PlaceOrder()
    {
        Console.WriteLine("Customer placed an order.");
    }

    public void TrackOrder()
    {
        Console.WriteLine("Customer is tracking their order.");
    }

    public void DeliverOrder()
    {
        throw new NotImplementedException("Customer cannot deliver orders.");
    }
}

public class DeliveryPerson : IUser
{
    public void PlaceOrder()
    {
        throw new NotImplementedException("Delivery person cannot place orders.");
    }

    public void TrackOrder()
    {
        throw new NotImplementedException("Delivery person cannot track orders.");
    }

    public void DeliverOrder()
    {
        Console.WriteLine("Delivery person delivered the order.");
    }
}
