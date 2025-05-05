namespace ExpenseTracker.Schema;

public class ExpenseDetailRequest{
  // public int ExpenseId {get; set;}
  public decimal Amount {get; set;}
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;}  // Bu alanda görsel yükleme olcak bunu Hatay Bul projesinden bakıcam
}

public class ExpenseDetailResponse
{  
  public decimal Amount {get; set;}
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;} 
  public string IsState {get; set;}
}
