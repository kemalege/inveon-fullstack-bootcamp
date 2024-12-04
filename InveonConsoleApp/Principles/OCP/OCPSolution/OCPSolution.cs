namespace InveonConsoleApp.Principles.OCP.OCPSolution;

public interface IPaymentMethod
{
    void ProcessPayment();
}

public class CreditCard : IPaymentMethod
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing credit card payment...");
    }
}

public class PayPal : IPaymentMethod
{
    public void ProcessPayment()
    {
        Console.WriteLine("Processing PayPal payment...");
    }
}

public class PaymentProcessor(IPaymentMethod paymentMethod)
{
    private readonly IPaymentMethod _paymentMethod = paymentMethod;

    public void Process()
    {
        _paymentMethod.ProcessPayment();
    }
}
