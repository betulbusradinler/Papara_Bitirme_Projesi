
namespace ExpenseTracker.Api.Service;
public class PaymentService : IPaymentService
{
    public async Task<PaymentResult> SimulatePaymentAsync(decimal Amount, string FirstName, string LastName)
    {
        await Task.Delay(50);

        return new PaymentResult
        {
            Success = true,
            Message = $"Simulated EFT to personnel {FirstName + " " + LastName} for amount {Amount} tl completed."
        };
    }
}
public class PaymentResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
}