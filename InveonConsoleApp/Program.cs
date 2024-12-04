using System;
using InveonConsoleApp.Principles.DIP.DIPSolution;
using InveonConsoleApp.Principles.ISP.ISPSolution;
using InveonConsoleApp.Principles.LSP.LSPSolution;
using InveonConsoleApp.Principles.OCP.OCPSolution;
using InveonConsoleApp.Principles.SRP.WithSRP;

using ViolationNotification = InveonConsoleApp.Principles.DIP.DIPViolation.Notification;
using DipNotification = InveonConsoleApp.Principles.DIP.DIPSolution.Notification;


namespace InveonBootcamp
{
    class Program
    {
        static void Main(string[] args)
        {
            var notificationService = new NotificationService();
            var mediator = new UserMediator(notificationService);

            var userManager = new UserManager(mediator);
            userManager.CreateUser("kemal_ege");
            
            // var processor = new PaymentProcessor();
            // processor.ProcessPayment("CreditCard");
            // processor.ProcessPayment("PayPal");
            
            var paypalPayment = new PayPal();
            var paypalProcessor = new PaymentProcessor(paypalPayment);
            paypalProcessor.Process();
            
            IMotorVehicle car = new Car();
            car.StartEngine();
            car.Move();

            INonMotorVehicle bicycle = new Bicycle();
            bicycle.StartPedaling();
            bicycle.Move();
            
            ICustomerActions customer = new Customer();
            customer.PlaceOrder();
            customer.TrackOrder();

            IDeliveryActions deliveryPerson = new DeliveryPerson();
            deliveryPerson.DeliverOrder();
            
            var notification = new ViolationNotification();
            notification.Send("Hello, DIP violation!");
            
            var emailNotification = new DipNotification(new EmailSender());
            emailNotification.Send("Hello via Email!");

            var smsNotification = new DipNotification(new SmsSender());
            smsNotification.Send("Hello via SMS!");


        }
    }
}