using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllPersonnelQuery:IRequest<ApiResponse<List<PersonnelResponse>>>;
public record GetAllPersonnelByIdQuery(int Id):IRequest<ApiResponse<PersonnelResponse>>;
public record CreatePersonnelCommand(PersonnelRequest PersonnelRequest) : IRequest<ApiResponse>;
public record UpdatePersonnelCommand(int Id, PersonnelRequest PersonnelRequest):IRequest<ApiResponse>;
public record DeletePersonnelCommand(int Id):IRequest<ApiResponse>;