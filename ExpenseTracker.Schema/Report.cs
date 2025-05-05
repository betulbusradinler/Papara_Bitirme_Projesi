
namespace ExpenseTracker.Schema;
public class PersonnelExpenseReportDto
{
    public int ExpenseId { get; set; }
    public int StaffId { get; set; }
    public string FullName { get; set; }
    public decimal Amount { get; set; }
    public string Demand { get; set; }
    public DateTime PaymentDate { get; set; }
    public string CategoryName { get; set; }
}

public class ReportResponse
{
    public string PaymentCategoryName { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Demand { get; set; }
    public string Description { get; set; }
}