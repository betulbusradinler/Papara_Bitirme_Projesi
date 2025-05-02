using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllPaymentCategoryQuery : IRequest<ApiResponse<List<PaymentCategoryResponse>>>;
public record GetPaymentCategoryByIdQuery(int Id) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record CreatePaymentCategoryCommand(PaymentCategoryRequest PaymentCategoryRequest) : IRequest<ApiResponse<PaymentCategoryResponse>>;
public record UpdatePaymentCategoryCommand(int Id, PaymentCategoryRequest PaymentCategoryRequest) : IRequest<ApiResponse>;

public record DeletePaymentCategoryCommand(int Id) : IRequest<ApiResponse>;