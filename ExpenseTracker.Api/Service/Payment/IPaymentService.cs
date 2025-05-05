namespace ExpenseTracker.Api.Service;

public interface IPaymentService
{
    Task<PaymentResult> SimulatePaymentAsync(decimal Amount, string FirstName, string LastName);
}