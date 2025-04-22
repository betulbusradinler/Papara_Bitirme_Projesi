using Base;

namespace ExpenseTracker.Entity;
public class Staff:Personnel
{
    public List<Expense> Expense {get; set;}
}

