
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


public class ExpenseSummaryReportRequest
{
    public ReportType ReportType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ExpenseSummaryReportResponse
{
    public string DemandState { get; set; }
    public string CategoryName { get; set; }
    public decimal SumExpense { get; set; }
    public int Weekly { get; set; }
    public int Monthly { get; set; }
    public int Year { get; set; }
    public DateTime Daily { get; set; }

}

public enum ReportType
{
    Daily = 1,
    WeeklyAndMonthly = 2
}
