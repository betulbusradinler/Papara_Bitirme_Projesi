namespace ExpenseTracker.Base.Domain;

public class BaseEntity{
    public int Id {get; set;}
    public string CreatedUser {get; set;}
    public string UpdatedUser {get; set;}
    public bool IsActive { get; set;}
    public DateTime CreatedDate {get; set;}
    public DateTime? UpdatedDate {get; set;}
    
}