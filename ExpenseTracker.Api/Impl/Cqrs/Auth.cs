using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record CreateAuthTokenCommand(AuthRequest Auth) : IRequest<ApiResponse<AuthResponse>>;