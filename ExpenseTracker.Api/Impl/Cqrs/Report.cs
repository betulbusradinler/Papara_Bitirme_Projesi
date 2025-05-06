using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using MediatR;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetPersonnelExpenseReportQuery : IRequest<ApiResponse<List<PersonnelExpenseReportDto>>>;
public record GetCompanyPaymentReportQuery(PaymentSummaryRequest PaymentSummary) : IRequest<ApiResponse<List<PaymentSummaryResponse>>>;
public record GetAllExpenseReportQuery(ExpenseSummaryRequest ExpenseSummary) : IRequest<ApiResponse<List<ExpenseSummaryResponse>>>;

