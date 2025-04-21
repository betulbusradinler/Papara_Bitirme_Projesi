namespace Base;

public class BaseEntity{
    public int Id {get; set;}
    public string CreatedPersonnel {get; set;}
    public string UpdatedPersonnel {get; set;}
    public bool IsActive { get; set;}
    public DateTime CreatedDate {get; set;}
    public DateTime UpdatedDate {get; set;}
    
}