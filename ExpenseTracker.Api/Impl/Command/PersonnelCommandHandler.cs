using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Impl.UnitOfWork;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;

namespace ExpenseTracker.Api.Impl.Command;

public class PersonnelCommandHandler:IRequestHandler<CreatePersonnelCommand, ApiResponse>,
IRequestHandler<UpdatePersonnelCommand, ApiResponse>,
IRequestHandler<DeletePersonnelCommand, ApiResponse>
{
    private readonly IMapper mapper;
    private readonly IAppSession appSession;
    private readonly IUnitOfWork unitOfWork;
    public PersonnelCommandHandler(IMapper mapper, IAppSession appSession, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.appSession = appSession;
        this.unitOfWork = unitOfWork;
    }
    public async Task<ApiResponse> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
    {
        if(request.PersonnelRequest.Password != request.PersonnelRequest.PasswordConfirm)
            return new ApiResponse("Şifreler Eşleşmiyor", 400);

        var mapped = mapper.Map<Personnel>(request.PersonnelRequest);  

        var entity = await unitOfWork.PersonnelRepository.AddAsync(mapped);
        await unitOfWork.Complete();

        mapper.Map<PersonnelResponse>(entity);

        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdatePersonnelCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PersonnelRepository.GetByIdAsync(request.Id);
        if (entity == null || !entity.IsActive)
            return new ApiResponse("Personel kayıtlı veya aktif değil", 400);
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
        var entity = await unitOfWork.PersonnelRepository.GetByIdAsync(request.Id);
        if (entity == null || !entity.IsActive)
            return new ApiResponse("Personel kayıtlı veya aktif değil", 400);

        entity.IsActive = false;

        unitOfWork.PersonnelRepository.Update(entity);
        await unitOfWork.Complete();
        
        return new ApiResponse();
    }

}