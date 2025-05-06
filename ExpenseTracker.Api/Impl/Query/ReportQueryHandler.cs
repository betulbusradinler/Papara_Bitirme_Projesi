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
IRequestHandler<GetAllExpenseReportQuery, ApiResponse<List<ExpenseSummaryResponse>>>
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
        if (personnelId <= 0)
            return new ApiResponse<List<PersonnelExpenseReportDto>>("Lütfen Giriş yapın", 401);

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
        // if (request.PaymentSummary.StartDate == null || request.PaymentSummary.StartDate >= DateTime.Now || request.PaymentSummary.EndDate >= DateTime.Now)
        //     return new ApiResponse<List<PaymentSummaryResponse>>("Başlangıç tarihi boş olamaz. Başlangıç ve Bitiş tarihi bugünden büyük olamaz", 400);

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

    public async Task<ApiResponse<List<ExpenseSummaryResponse>>> Handle(GetAllExpenseReportQuery request, CancellationToken cancellationToken)
    {
        // if (request.ExpenseSummary.StartDate == null || request.ExpenseSummary.StartDate > DateTime.Now || request.ExpenseSummary.EndDate > DateTime.Now)
        //     return new ApiResponse<List<ExpenseSummaryResponse>>("Başlangıç tarihi boş olamaz. Başlangıç ve Bitiş tarihi bugünden büyük olamaz", 400);

        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", request.ExpenseSummary.StartDate);
        parameters.Add("@EndDate", request.ExpenseSummary.EndDate ?? request.ExpenseSummary.StartDate);

        using var connection = dapperContext.CreateConnection();

        IEnumerable<ExpenseSummaryResponse> result = request.ExpenseSummary.ReportType switch
        {
            "Daily" => await connection.QueryAsync<ExpenseSummaryResponse>("sp_GetDailyExpenseSummary", parameters, commandType: CommandType.StoredProcedure),
            "Weekly" => await connection.QueryAsync<ExpenseSummaryResponse>("sp_GetWeeklyExpenseSummary", parameters, commandType: CommandType.StoredProcedure),
            "Monthly" => await connection.QueryAsync<ExpenseSummaryResponse>("sp_GetMonthlyExpenseSummary", parameters, commandType: CommandType.StoredProcedure),
            _ => Enumerable.Empty<ExpenseSummaryResponse>()
        };

        var resultList = result.ToList();

        if (!resultList.Any())
            return new ApiResponse<List<ExpenseSummaryResponse>>("Bu tarihler arasında harcama bulunmuyor.", 400);

        return new ApiResponse<List<ExpenseSummaryResponse>>(resultList);
    }
}