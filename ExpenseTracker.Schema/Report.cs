
namespace ExpenseTracker.Schema;
public class PersonnelExpenseReportDto
{
    public string PaymentCategory { get; set; }
    public string? Description { get; set; }
    public string PaymentPoint { get; set; }
    public string PaymentInstrument { get; set; }
    public string Receipt { get; set; }
    public string RejectDescription { get; set; }
    public string Demand { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
public class PaymentSummaryResponse
{
    public DateTime PaymentDate { get; set; }
    public string PaymentPoint { get; set; }
    public string PaymentInstrument { get; set; }
    public string Receipt { get; set; }
    public decimal Amount { get; set; }
    public string CategoryName { get; set; }
}
public class PaymentSummaryRequest
{
    // public string Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ExpenseSummaryRequest
{
    public string ReportType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ExpenseSummaryResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PaymentCategoryName { get; set; }
    public string RejectDescription { get; set; }
    public string Demand { get; set; }
    public decimal Amount { get; set; }
    public string PaymentPoint { get; set; }
    public string PaymentInstrument { get; set; }
    public string Receipt { get; set; }

}
