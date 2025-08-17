using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Query;

public class PersonnelQueryHandler :
IRequestHandler<GetAllPersonnelQuery, ApiResponse<List<PersonnelResponse>>>,
IRequestHandler<GetExpensesByPersonnelIdQuery, ApiResponse<List<ExpenseResponse>>>,
IRequestHandler<GetPersonnelByIdQuery, ApiResponse<PersonnelResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAppSession appSession;
    private readonly IMapper mapper;

    public PersonnelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppSession appSession)
    {
        this.appSession = appSession;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PersonnelResponse>>> Handle(GetAllPersonnelQuery request, CancellationToken cancellationToken)
    {

        var Entity = await unitOfWork.PersonnelRepository.GetAllAsync();

        Entity.RemoveAll(p => p.IsActive == false);

        if (Entity.Count <= 0)
            return new ApiResponse<List<PersonnelResponse>>("Personel bulunamadı", 400);

        var mapped = mapper.Map<List<PersonnelResponse>>(Entity);

        return new ApiResponse<List<PersonnelResponse>>(mapped);
    }

    public async Task<ApiResponse<PersonnelResponse>> Handle(GetPersonnelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PersonnelRepository.GetPersonnelDetailById(request.Id);

        if (entity == null || !entity.IsActive)
            return new ApiResponse<PersonnelResponse>("Personel bulunamadı", 400);

        var mapped = mapper.Map<PersonnelResponse>(entity);
        //var jsonSerialize = JsonConvert.SerializeObject(mapped);
        return new ApiResponse<PersonnelResponse>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpensesByPersonnelIdQuery request, CancellationToken cancellationToken)
    {
        var convertUserID = Convert.ToInt32(appSession.PersonnelId);

        var user = await unitOfWork.PersonnelRepository.GetByIdAsync(convertUserID);
        if (user.Role == "Admin" && request.Id == null)
            return new ApiResponse<List<ExpenseResponse>>("Lütfen bir personel giriniz.", 400);

        convertUserID = request.Id ?? convertUserID;

        var entity = await unitOfWork.ExpenseRepository.GetExpensesByPersonnelIdAsync(convertUserID);

        entity.RemoveAll(p => p.IsActive == false);
        if (entity.Count <= 0)
            return new ApiResponse<List<ExpenseResponse>>("Harcama Bulunamadı", 400);

        var mapped = mapper.Map<List<ExpenseResponse>>(entity);
        return new ApiResponse<List<ExpenseResponse>>(mapped);
    }

}
