using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllPersonnelQuery:IRequest<ApiResponse<List<PersonnelResponse>>>;
public record GetPersonnelByIdQuery(int Id) : IRequest<ApiResponse<PersonnelResponse>>;
public record GetExpensesByPersonnelIdQuery(int? Id) : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record CreatePersonnelCommand(PersonnelRequest PersonnelRequest) : IRequest<ApiResponse>;
public record UpdatePersonnelCommand(int Id, PersonnelRequest PersonnelRequest):IRequest<ApiResponse>;
public record DeletePersonnelCommand(int Id):IRequest<ApiResponse>;