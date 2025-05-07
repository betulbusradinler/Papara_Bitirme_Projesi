using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using MediatR;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.DbOperations;
using Dapper;
using System.Data;

namespace ExpenseTracker.Api.Impl.Query;

public class ReportQueryHandler :
IRequestHandler<GetPersonnelExpenseReportQuery, ApiResponse<List<PersonnelExpenseReportDto>>>,
IRequestHandler<GetCompanyPaymentReportQuery, ApiResponse<List<PaymentSummaryResponse>>>,
IRequestHandler<GetAllExpenseReportQuery, ApiResponse<List<ExpenseSummaryReportResponse>>>
{
    private readonly DapperContext dapperContext;
    private readonly IAppSession appSession;
    public ReportQueryHandler(DapperContext dapperContext, IAppSession appSession)
    {
        this.dapperContext = dapperContext;
        this.appSession = appSession;
    }

    public async Task<ApiResponse<List<PersonnelExpenseReportDto>>> Handle(GetPersonnelExpenseReportQuery request, CancellationToken cancellationToken)
    {
        int personnelId = Convert.ToInt32(appSession.PersonnelId);

        var sql = "SELECT * FROM vw_PersonnelExpenses WHERE StaffId = @StaffId";

        using var connection = dapperContext.CreateConnection();
        var data = await connection.QueryAsync<PersonnelExpenseReportDto>(sql, new { StaffId = personnelId });

        if (data.ToList().Count <= 0)
            return new ApiResponse<List<PersonnelExpenseReportDto>>("Geçerli Harcama Listesi Mevcut Değil", 400);

        return new ApiResponse<List<PersonnelExpenseReportDto>>(data.ToList());
    }

    public async Task<ApiResponse<List<PaymentSummaryResponse>>> Handle(
    GetCompanyPaymentReportQuery request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", request.PaymentSummary.StartDate);
        parameters.Add("@EndDate", request.PaymentSummary.EndDate ?? request.PaymentSummary.StartDate);

        using var connection = dapperContext.CreateConnection();
        var result = await connection.QueryAsync<PaymentSummaryResponse>(
            "sp_GetPaymentSummaryDetails",
            parameters,
            commandType: CommandType.StoredProcedure
        );
        if (result.ToList().Count <= 0)
            return new ApiResponse<List<PaymentSummaryResponse>>(" Bu tarihler arasında harcama bulunmuyor ", 400);
        return new ApiResponse<List<PaymentSummaryResponse>>(result.ToList());
    }

    public async Task<ApiResponse<List<ExpenseSummaryReportResponse>>> Handle(GetAllExpenseReportQuery request, CancellationToken cancellationToken)
    {
        using var connection = dapperContext.CreateConnection();

        string storedProcedureName = request.ExpenseSummary.ReportType switch
        {
            ReportType.Daily => "sp_GetDailyExpenseSummary",
            ReportType.WeeklyAndMonthly => "sp_GetWeeklyAndMonthlyExpenseReport",
            _ => throw new ArgumentOutOfRangeException()
        };
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", request.ExpenseSummary.StartDate);
        parameters.Add("@EndDate", request.ExpenseSummary.EndDate ?? request.ExpenseSummary.StartDate);

        var result = await connection.QueryAsync<ExpenseSummaryReportResponse>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);

        var resultList = result.ToList();

        if (!resultList.Any())
            return new ApiResponse<List<ExpenseSummaryReportResponse>>("Bu tarihler arasında harcama bulunmuyor.", 400);

        return new ApiResponse<List<ExpenseSummaryReportResponse>>(resultList);
    }
}