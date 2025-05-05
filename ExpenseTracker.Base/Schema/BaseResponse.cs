namespace ExpenseTracker.Base;
public class BaseResponse
{
    public int Id { get; set; }
    public string CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }
}