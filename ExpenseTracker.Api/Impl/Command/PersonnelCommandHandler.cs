using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Impl.UnitOfWork;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelCommandHandler:IRequestHandler<CreatePersonnelCommand, ApiResponse>,
IRequestHandler<UpdatePersonnelCommand, ApiResponse>,
IRequestHandler<DeletePersonnelCommand, ApiResponse>
{
    private readonly ExpenseTrackDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IAppSession appSession;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelCommandHandler(ExpenseTrackDbContext dbContext, IMapper mapper, IAppSession appSession, IUnitOfWork unitOfWork)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.appSession = appSession;
        this.unitOfWork = unitOfWork;
    }
    // User Register Rolü 1 Admin olanlar bu işlemleri yapabilir
    // Password Validation Eklenebilir.
    public async Task<ApiResponse> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
    {
        if(request.PersonnelRequest.Password != request.PersonnelRequest.PasswordConfirm)
            return new ApiResponse("Şifreler Eşleşmiyor");

        var mapped = mapper.Map<Personnel>(request.PersonnelRequest);  

        var entity = await dbContext.AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<PersonnelResponse>(entity.Entity);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PersonnelRepository.GetByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse("Personnel not found");

        if (!entity.IsActive)
            return new ApiResponse("Personnel is not active");
        
        entity.UserName = request.PersonnelRequest.UserName;
        entity.FirstName = request.PersonnelRequest.FirstName;
        entity.LastName = request.PersonnelRequest.LastName;
        entity.Email = request.PersonnelRequest.Email;

        unitOfWork.PersonnelRepository.Update(entity);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePersonnelCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PersonnelAddressRepository.GetByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse("PersonnelAddress not found");

        if (!entity.IsActive)
            return new ApiResponse("Personnel is not active");

        entity.IsActive = false;

        unitOfWork.PersonnelAddressRepository.Update(entity);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

}