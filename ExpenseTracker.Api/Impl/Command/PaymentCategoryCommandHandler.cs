using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.UnitOfWork;

namespace ExpenseTracker.Api.Impl.Command;

public class PaymentCategoryCommandHandler :
IRequestHandler<CreatePaymentCategoryCommand, ApiResponse<PaymentCategoryResponse>>,
IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
IRequestHandler<DeletePaymentCategoryCommand, ApiResponse> 
{
    private readonly  IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private const string PaymentCategoryNotFoundMessage = "Harcama bulunamadı veya aktif değil";
    public PaymentCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(CreatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<PaymentCategory>(request.PaymentCategoryRequest);

        var entity = await unitOfWork.PaymentCategoryRepository.AddAsync(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<PaymentCategoryResponse>(entity);
       
        return new ApiResponse<PaymentCategoryResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.Id);
        if (entity == null || !entity.IsActive)
            return new ApiResponse(PaymentCategoryNotFoundMessage, 400);

        entity.Name = request.PaymentCategoryRequest.Name;
        unitOfWork.PaymentCategoryRepository.Update(entity);
        await unitOfWork.Complete();

        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
      var entity = await  unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.Id);

        if (entity == null || !entity.IsActive)
            return new ApiResponse(PaymentCategoryNotFoundMessage, 400);

        await unitOfWork.PaymentCategoryRepository.DeleteByIdAsync(entity.Id);
        await unitOfWork.Complete();

        return new ApiResponse();
    }
}
