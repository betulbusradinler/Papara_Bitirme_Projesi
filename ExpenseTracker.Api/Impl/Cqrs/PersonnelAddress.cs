using MediatR;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Cqrs;

public record GetAllPersonnelAddressQuery:IRequest<ApiResponse<List<PersonnelAddressResponse>>>;
public record GetAllPersonnelAddressByIdQuery(int Id):IRequest<ApiResponse<PersonnelAddressResponse>>;
public record CreatePersonnelAddressCommand(int Id, PersonnelAddressRequest PersonnelAddressRequest) : IRequest<ApiResponse>;
public record UpdatePersonnelAddressCommand(int Id, PersonnelAddressRequest Request):IRequest<ApiResponse>;
public record DeletePersonnelAddressCommand(int Id):IRequest<ApiResponse>;