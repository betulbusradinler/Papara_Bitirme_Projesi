using Base;
namespace ExpenseTracker.Entity;

//Masraf talebi için kategori bilgisi olmalı. Talep edilen ödeme kategorisi ödeme aracı ödeme yapılan konum ve fiş yada fatura vb dökümanlar sisteme yüklenebilmeli.

public class PaymentCategory:BaseEntity
{
  public string Name {get; set;} 
  public List<Expense> Expense {get; set;}s
}
