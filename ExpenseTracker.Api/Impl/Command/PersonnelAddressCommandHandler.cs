using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelAddressCommandHandler:IRequestHandler<CreatePersonnelAddressCommand, ApiResponse>
{

    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelAddressCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(CreatePersonnelAddressCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<PersonnelAddress>(request.PersonnelAddressRequest);

        var entity = await unitOfWork.PersonnelAddressRepository.AddAsync(mapped);
        await unitOfWork.Complete();
        var response = mapper.Map<PersonnelAddressResponse>(entity);
        return new ApiResponse();
    }


}