using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelAddressCommandHandler:IRequestHandler<CreatePersonnelAddressCommand, ApiResponse>{

    private readonly ExpenseTrackDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelAddressCommandHandler(ExpenseTrackDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(CreatePersonnelAddressCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<PersonnelAddress>(request.PersonnelAddressRequest);
        mapped.IsActive = true;
        mapped.PersonnelId = request.Id;

        var entity = await unitOfWork.PersonnelAddressRepository.AddAsync(mapped);
        await unitOfWork.Complete();
        var response = mapper.Map<PersonnelAddressResponse>(entity);
        return new ApiResponse();
        // var mapped = mapper.Map<PersonnelAddress>(request.PersonnelAddressRequest);
        // var entity = await dbContext.AddAsync(mapped, cancellationToken);
        // await dbContext.SaveChangesAsync(cancellationToken);
        // var response = mapper.Map<PersonnelAddressResponse>(entity.Entity);
        // return new ApiResponse("Ekleme Başarılı");
    }


}