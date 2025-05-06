namespace ExpenseTracker.Schema;

public class ExpenseDetailRequest
{
  public decimal Amount { get; set; }
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt { get; set; }
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
