using Base.Schema;
namespace ExpenseTracker.Schema;
public class ExpenseDetailResponse:BaseResponse
{  
  public int PaymentCategoryId {get; set;} 
  // Çalışan Id Sessiondan gelcek
  public int StaffId {get; set;}
  public decimal Amount {get; set;}
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;} 
  public string IsState {get; set;}
}
