using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class PaymentCategoryRequest
{
  public string Name {get; set;} 
}

public class PaymentCategoryResponse:BaseResponse
{
  public string Name {get; set;} 
}
