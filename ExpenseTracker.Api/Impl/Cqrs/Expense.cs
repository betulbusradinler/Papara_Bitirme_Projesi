using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllExpenseQuery : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int Id) : IRequest<ApiResponse<ExpenseDetailResponse>>;
public record CreateExpenseCommand(ExpenseRequest ExpenseRequest) : IRequest<ApiResponse>;
public record UpdateExpenseCommand(int Id, ExpenseRequest ExpenseRequest) : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int Id) : IRequest<ApiResponse>;