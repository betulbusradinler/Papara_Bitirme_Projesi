using Base;
namespace ExpenseTracker.Entity;

//Masraf talebi için kategori bilgisi olmalı. Talep edilen ödeme kategorisi ödeme aracı ödeme yapılan konum ve fiş yada fatura vb dökümanlar sisteme yüklenebilmeli.

public class Expense:BaseEntity
{
  public int PaymentCategoryId {get; set;}
  public int StaffId {get; set;}
  public int DemandsId {get; set;}
  public int ExpenseDetailId {get; set;}
  
  // Navigation Property
  public Demands Demands {get; set;}
  public Staff Staff {get; set;}
  public PaymentCategory paymentCategory {get; set;}
  public List<ExpenseDetail> expenseDetail {get; set;}

}
