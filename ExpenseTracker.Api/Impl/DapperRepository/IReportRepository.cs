using ExpenseTracker.Schema;

namespace ExpenseTracker.Api.Impl.DapperRepository;
public interface IReportRepository
{
    Task<IEnumerable<ReportResponse>> GetPersonnelExpensesAsync(int personnelId);
}
