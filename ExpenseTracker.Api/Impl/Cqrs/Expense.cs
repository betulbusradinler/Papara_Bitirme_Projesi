using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllExpenseQuery : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int Id) : IRequest<ApiResponse<ExpenseResponse>>;
public record GetFilteredExpensesQuery(ExpenseFilterRequest Filter) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetAllPersonnelExpenseQuery() : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record CreateExpenseCommand(ExpenseRequest ExpenseRequest) : IRequest<ApiResponse<ExpenseResponse>>;
public record CreateExpenseListCommand(List<ExpenseRequest> ExpenseRequests) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record UpdateExpenseCommand(int Id, ExpenseRequest ExpenseRequest) : IRequest<ApiResponse>;
public record ApproveOrRejectExpenseCommand(int Id, ApproveOrRejectExpenseRequest approveOrRejectExpense) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>;