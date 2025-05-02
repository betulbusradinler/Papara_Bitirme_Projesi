using ExpenseTracker.Base;
namespace ExpenseTracker.Schema;

public class ExpenseRequest
{
 // böyle bir payment category var mı control etmeliyim 
  public int PaymentCategoryId {get; set;} 
  // Çalışan Id Sessiondan gelcek
  public int StaffId {get; set;}
  public decimal Amount {get; set;}
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;}  // Bu alanda görsel yükleme olcak bunu Hatay Bul projesinden bakıcam
}

public class ExpenseResponse:BaseResponse
{  
  public int PaymentCategoryId {get; set;} 
  // Çalışan Id Sessiondan gelcek
  public int StaffId {get; set;}
  public decimal Amount {get; set;}
  public string IsState {get; set;}
}
