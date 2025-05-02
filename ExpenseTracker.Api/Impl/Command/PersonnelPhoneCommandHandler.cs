using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Impl.UnitOfWork;
using ExpenseTracker.Schema;
using ExpenseTracker.Base;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelPhoneCommandHandler:IRequestHandler<CreatePersonnelPhoneCommand, ApiResponse>{

    private readonly ExpenseTrackDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelPhoneCommandHandler(ExpenseTrackDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.dbContext = dbContext;
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
        // var mapped = mapper.Map<PersonnelPhone>(request.PersonnelPhoneRequest);
        // var entity = await dbContext.AddAsync(mapped, cancellationToken);
        // await dbContext.SaveChangesAsync(cancellationToken);
        // var response = mapper.Map<PersonnelPhoneResponse>(entity.Entity);
        // return new ApiResponse("Ekleme Başarılı");
    }


}