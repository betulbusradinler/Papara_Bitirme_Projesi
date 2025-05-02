using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllPersonnelPhoneQuery:IRequest<ApiResponse<List<PersonnelPhoneResponse>>>;
public record GetAllPersonnelPhoneByIdQuery(int Id):IRequest<ApiResponse<PersonnelPhoneResponse>>;
public record CreatePersonnelPhoneCommand(PersonnelPhoneRequest PersonnelPhoneRequest) : IRequest<ApiResponse>;
public record UpdatePersonnelPhoneCommand(int Id, PersonnelPhoneRequest Request):IRequest<ApiResponse>;
public record DeletePersonnelPhoneCommand(int Id):IRequest<ApiResponse>;