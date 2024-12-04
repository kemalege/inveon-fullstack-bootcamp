namespace InveonConsoleApp.Principles.OCP.OCPViolation;

public class PaymentProcessor
{
    public void ProcessPayment(string paymentMethod)
    {
        if (paymentMethod == "CreditCard")
        {
            Console.WriteLine("Processing credit card payment...");
        }
        else if (paymentMethod == "PayPal")
        {
            Console.WriteLine("Processing PayPal payment...");
        }
        else
        {
            throw new Exception("Unsupported payment method.");
        }
    }
}