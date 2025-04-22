using Base;
namespace ExpenseTracker.Entity;

//Masraf talebi için kategori bilgisi olmalı. Talep edilen ödeme kategorisi ödeme aracı ödeme yapılan konum ve fiş yada fatura vb dökümanlar sisteme yüklenebilmeli.

public class ExpenseDetail:BaseEntity
{
  public int ExpenseId {get; set;}
  public decimal Amount {get; set;}
  public string Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;}  
  
  public Expense Expense {get; set;}
}
