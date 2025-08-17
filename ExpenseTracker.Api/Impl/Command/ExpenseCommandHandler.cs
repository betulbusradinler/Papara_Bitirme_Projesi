using AutoMapper;
using MediatR;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Base;
using ExpenseTracker.Api.Impl.UnitOfWork;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Service;

namespace ExpenseTracker.Api.Impl.Command;

public class ExpenseCommandHandler :
IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
IRequestHandler<CreateExpenseListCommand, ApiResponse<List<ExpenseResponse>>>,
IRequestHandler<UpdateExpenseCommand, ApiResponse>,
IRequestHandler<ApproveOrRejectExpenseCommand, ApiResponse>,
IRequestHandler<DeleteExpenseCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IAppSession appSession;
    private readonly IPaymentService paymentService;
    private const string ExpenseNotFoundMessage = "Harcama bulunamadı veya aktif değil";
    private const string InvalidStateMessage = "Geçersiz Harcama Onaylama Değeri";
    private const string PaymentCategoryNotFoundMessage = "Geçersiz Harcama Onaylama Değeri";
    public ExpenseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppSession appSession, IPaymentService paymentService)
    {
        this.appSession = appSession;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.paymentService = paymentService;
    }
    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var paymentCategoryExist = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.ExpenseRequest.PaymentCategoryId);
        if (paymentCategoryExist == null)
            return new ApiResponse<ExpenseResponse>(ExpenseNotFoundMessage, 400);

        var mapped = mapper.Map<Expense>(request.ExpenseRequest);

        mapped.PaymentCategoryId = paymentCategoryExist.Id;
        mapped.StaffId = Convert.ToInt32(appSession.PersonnelId);

        var entity = await unitOfWork.ExpenseRepository.AddAsync(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<ExpenseResponse>(entity);
        response.DemandState = entity.Demand.ToString();
        return new ApiResponse<ExpenseResponse>(response);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(CreateExpenseListCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<List<Expense>>(request.ExpenseRequests);

        var entity = await unitOfWork.ExpenseRepository.AddRangeAsync(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<List<ExpenseResponse>>(entity);

        return new ApiResponse<List<ExpenseResponse>>(response);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var paymentCategoryExist = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.ExpenseRequest.PaymentCategoryId);
        if (paymentCategoryExist == null)
            return new ApiResponse(PaymentCategoryNotFoundMessage, 400);

        var entity = await unitOfWork.ExpenseRepository.GetExpenseWithDetailByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse(ExpenseNotFoundMessage, 400);

        if (!entity.IsActive)
            return new ApiResponse(InvalidStateMessage, 400);

        if (entity.Demand != 0)
            return new ApiResponse("Harcama Onaylandığı İçin Güncellenemez.");

        entity.PaymentCategoryId = request.ExpenseRequest.PaymentCategoryId;
        entity.ExpenseDetail.Amount = request.ExpenseRequest.ExpenseDetailRequest.Amount;
        entity.ExpenseDetail.Description = request.ExpenseRequest.ExpenseDetailRequest.Description;
        entity.ExpenseDetail.PaymentPoint = request.ExpenseRequest.ExpenseDetailRequest.PaymentPoint;
        entity.ExpenseDetail.PaymentInstrument = request.ExpenseRequest.ExpenseDetailRequest.PaymentInstrument;
        entity.ExpenseDetail.Receipt = request.ExpenseRequest.ExpenseDetailRequest.Receipt;

        unitOfWork.ExpenseRepository.Update(entity);
        await unitOfWork.Complete();

        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(ApproveOrRejectExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ExpenseRepository.GetExpenseWithDetailByIdAsync(request.Id);
        if (entity == null || !entity.IsActive)
            return new ApiResponse(ExpenseNotFoundMessage, 400);

        var personnelExistUser = await GetCurrentPersonnelAsync();

        if (!Enum.TryParse<DemandState>(request.approveOrRejectExpense.DemandState.ToString(), out var demandState))
            return new ApiResponse(InvalidStateMessage, 400);

        entity.Demand = demandState;

        if (demandState == DemandState.Onaylandı)
            return await HandleApproval(entity, personnelExistUser);

        if (demandState == DemandState.Reddedildi)
            entity.RejectDescription = request.approveOrRejectExpense.Description;

        unitOfWork.ExpenseRepository.Update(entity);
        await unitOfWork.Complete();

        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ExpenseRepository.GetByIdAsync(request.Id);

        if (entity == null || !entity.IsActive)
            return new ApiResponse(ExpenseNotFoundMessage, 400);
        if (entity.Demand != 0)
            return new ApiResponse("Harcama Onaylandığı veya Reddedildiği İçin Silinemez", 400);

        await unitOfWork.ExpenseRepository.DeleteByIdAsync(entity.Id);
        await unitOfWork.Complete();

        return new ApiResponse();
    }

    private async Task<Personnel?> GetCurrentPersonnelAsync()
    {
        int personnelId = Convert.ToInt32(appSession.PersonnelId);
        return await unitOfWork.PersonnelRepository.GetByIdAsync(personnelId);
    }
    private async Task<ApiResponse> HandleApproval(Expense entity, Personnel personnel)
    {
        var result = await paymentService.SimulatePaymentAsync(entity.ExpenseDetail.Amount, personnel.FirstName, personnel.LastName);
        if (result.Success)
        {
            var mapped = new Payment
            {
                ExpenseId = entity.Id,
                PaymentDate = DateTime.Now
            };
            var payment = await unitOfWork.PaymentRepository.AddAsync(mapped);
            await unitOfWork.Complete();

            return payment != null ? new ApiResponse() : new ApiResponse($"Payment not Registered", 500);
        }
        else
            return new ApiResponse($"Payment failed: {result.Message}", 402);
    }

}
