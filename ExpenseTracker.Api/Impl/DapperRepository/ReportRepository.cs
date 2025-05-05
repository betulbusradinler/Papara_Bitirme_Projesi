using Dapper;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Schema;

namespace ExpenseTracker.Api.Impl.DapperRepository;
public class ReportRepository : IReportRepository
{
    private readonly DapperContext dapperContext;

    public ReportRepository(DapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }
    public async Task<IEnumerable<ReportResponse>> GetPersonnelExpensesAsync(int personnelId)
    {
        var query = @"SELECT * FROM vw_PersonnelExpenses WHERE StaffId = @StaffId";

        using var connection = dapperContext.CreateConnection();
        var result = await connection.QueryAsync<ReportResponse>(query, new { StaffId = personnelId });

        return result;
    }
}