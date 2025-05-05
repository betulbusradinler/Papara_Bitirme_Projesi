using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.UnitOfWork;
using Newtonsoft.Json;

namespace ExpenseTracker.Api.Impl.Query;

public class PersonnelQueryHandler :
IRequestHandler<GetAllPersonnelQuery, ApiResponse<List<PersonnelResponse>>>,
IRequestHandler<GetPersonnelByIdQuery, ApiResponse<PersonnelResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public PersonnelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PersonnelResponse>>> Handle(GetAllPersonnelQuery request, CancellationToken cancellationToken)
    {

        var Entity = await unitOfWork.PersonnelRepository.GetAllAsync();

        Entity.RemoveAll(p => p.IsActive == false);

        if (Entity.Count <= 0)
            return new ApiResponse<List<PersonnelResponse>>("Personnel not found");

        var mapped = mapper.Map<List<PersonnelResponse>>(Entity);

        return new ApiResponse<List<PersonnelResponse>>(mapped);
    }

    public async Task<ApiResponse<PersonnelResponse>> Handle(GetPersonnelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PersonnelRepository.GetPersonnelDetailById(request.Id);

        if (entity == null || !entity.IsActive)
            return new ApiResponse<PersonnelResponse>("Personnel not found");

        var mapped = mapper.Map<PersonnelResponse>(entity);
        //var jsonSerialize = JsonConvert.SerializeObject(mapped);
        return new ApiResponse<PersonnelResponse>(mapped);
    }
}
