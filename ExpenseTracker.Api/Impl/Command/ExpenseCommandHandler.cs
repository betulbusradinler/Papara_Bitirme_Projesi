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
    private const string UnauthorizedMessage = "You are not authorized to perform this action.";
    private const string ExpenseNotFoundMessage = "Expense not found or inactive.";
    private const string InvalidStateMessage = "Invalid expense demand state.";
    public ExpenseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppSession appSession, IPaymentService paymentService)
    {
        this.appSession = appSession;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.paymentService = paymentService;
    }
    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var personnelExistUser = await GetCurrentPersonnelAsync();
        if (personnelExistUser == null)
            return new ApiResponse<ExpenseResponse>(UnauthorizedMessage);

        var paymentCategoryExist = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.ExpenseRequest.PaymentCategoryId);
        if (paymentCategoryExist == null)
            return new ApiResponse<ExpenseResponse>("Payment category is not exist");

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
        var personnelExistUser = await GetCurrentPersonnelAsync();
        if (personnelExistUser == null)
            return new ApiResponse(UnauthorizedMessage);

        var paymentCategoryExist = await unitOfWork.PaymentCategoryRepository.GetByIdAsync(request.ExpenseRequest.PaymentCategoryId);
        if (paymentCategoryExist == null)
            return new ApiResponse("Payment category is not exist");

        var entity = await unitOfWork.ExpenseRepository.GetExpenseWithDetailByIdAsync(request.Id);
        if (entity == null)
            return new ApiResponse("Expense not found");

        if (!entity.IsActive)
            return new ApiResponse("Expense is not active");

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

    // buranın yetkisi adminde 
    public async Task<ApiResponse> Handle(ApproveOrRejectExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.ExpenseRepository.GetExpenseWithDetailByIdAsync(request.Id);
        if (entity == null || !entity.IsActive)
            return new ApiResponse(ExpenseNotFoundMessage);

        var personnelExistUser = await GetCurrentPersonnelAsync();
        if (personnelExistUser == null)
            return new ApiResponse(UnauthorizedMessage);

        if (!Enum.TryParse<DemandState>(request.approveOrRejectExpense.DemandState.ToString(), out var demandState))
            return new ApiResponse(InvalidStateMessage);

        entity.Demand = demandState;

        if (demandState == DemandState.OnayBekliyor)
            return new ApiResponse("Ödeme ya onaylanmalı ya da reddedilmeli");

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

        if (entity == null)
            return new ApiResponse("Expense not found");

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

            return payment != null ? new ApiResponse() : new ApiResponse($"Payment not Registered");
        }
        else
            return new ApiResponse($"Payment failed: {result.Message}");
    }

}
