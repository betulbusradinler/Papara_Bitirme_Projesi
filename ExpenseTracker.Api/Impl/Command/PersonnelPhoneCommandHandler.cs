using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Impl.UnitOfWork;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelPhoneCommandHandler:IRequestHandler<CreatePersonnelPhoneCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelPhoneCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(CreatePersonnelPhoneCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<PersonnelPhone>(request.PersonnelPhoneRequest);
        mapped.IsActive = true;

        var entity = await unitOfWork.PersonnelPhoneRepository.AddAsync(mapped);
        await unitOfWork.Complete();
        var response = mapper.Map<PersonnelPhoneResponse>(entity);

        return new ApiResponse();
    }


}