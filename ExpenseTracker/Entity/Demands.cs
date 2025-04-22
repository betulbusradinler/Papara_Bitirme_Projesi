using Base;
namespace ExpenseTracker.Entity;

// Burada talepler için gerekli alanı girmem gerekiyor
public class Demands:BaseEntity
{
 public int ExpenseId {get; set;}
 public DemandsState IsState {get; set;}
 public  Expense Expense {get; set;}
}