using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Command;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Query;

public class PaymentCategoryQueryHandler : 
IRequestHandler<GetAllPaymentCategoryQuery, ApiResponse<List<PaymentCategoryResponse>>>, 
IRequestHandler<GetPaymentCategoryByIdQuery, ApiResponse<PaymentCategoryResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public PaymentCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetAllPaymentCategoryQuery request, CancellationToken cancellationToken)
    {
       
        var Entity = await unitOfWork.PaymentCategoryRepository.GetAllAsync();

        Entity.RemoveAll(p=>p.IsActive == false);

        if (Entity.Count<=0)
            return new ApiResponse<List<PaymentCategoryResponse>>("PaymentCategory not found");

        var mapped = mapper.Map<List<PaymentCategoryResponse>>(Entity);

        return new ApiResponse<List<PaymentCategoryResponse>>(mapped);
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(GetPaymentCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.Id);

        if (entity == null)
            return new ApiResponse<PaymentCategoryResponse>("PaymentCategory not found");

        if (!entity.IsActive)
            return new ApiResponse<PaymentCategoryResponse>("PaymentCategory is not active");

        var mapped = mapper.Map<PaymentCategoryResponse>(entity);
        
        return new ApiResponse<PaymentCategoryResponse>(mapped);
    }
}
