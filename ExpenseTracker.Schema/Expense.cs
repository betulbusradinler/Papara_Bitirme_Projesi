using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class ExpenseRequest
{
  public int PaymentCategoryId {get; set;}

  public ExpenseDetailRequest ExpenseDetailRequest { get; set; }
}

public class ExpenseResponse:BaseResponse
{
  public int Id { get; set; }
  public int PaymentCategoryId { get; set; }
  public int StaffId {get; set;}
  public decimal Amount {get; set;}
  public string DemandState { get; set; }
  public ExpenseDetailResponse ExpenseDetailResponse { get; set; }
}
public class ExpenseFilterRequest
{
  public int? PaymentCategoryId { get; set; }
  public int? DemandState { get; set; }
  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }
}
