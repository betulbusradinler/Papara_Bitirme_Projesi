using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain;
using ExpenseTracker.DbOperations;
using ExpenseTracker.Impl.Cqrs;
using ExpenseTracker.Base.ApiResponse;
using ExpenseTracker.Schema;

namespace ExpenseTracker.Impl.Command;

public class PaymentCategoryCommandHandler :
IRequestHandler<CreatePaymentCategoryCommand, ApiResponse<PaymentCategoryResponse>>,
IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
IRequestHandler<DeletePaymentCategoryCommand, ApiResponse> 
{
    private readonly ExpenseTrackDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCategoryCommandHandler(ExpenseTrackDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(CreatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<PaymentCategory>(request.PaymentCategoryRequest);

        var Entity = await dbContext.AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        var response = mapper.Map<PaymentCategoryResponse>(Entity.Entity);
       
        return new ApiResponse<PaymentCategoryResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
        
        var Entity = await dbContext.Set<PaymentCategory>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (Entity == null)
            return new ApiResponse("PaymentCategory not found");

        if (!Entity.IsActive)
            return new ApiResponse("PaymentCategory is not active");

        Entity.Name = request.PaymentCategoryRequest.Name;

        await dbContext.SaveChangesAsync(cancellationToken);
       
        return new ApiResponse();
    }

    
    public async Task<ApiResponse> Handle(DeletePaymentCategoryCommand request, CancellationToken cancellationToken)
    {
      var Entity = await dbContext.Set<PaymentCategory>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (Entity == null)
            return new ApiResponse("PaymentCategory not found");

        if (!Entity.IsActive)
            return new ApiResponse("PaymentCategory is not active");

        Entity.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);
     
        return new ApiResponse();
    }
}

/* IRequestHandler<UpdatePaymentCategoryCommand, ApiResponse>,
IRequestHandler<DeletePaymentCategoryCommand, ApiResponse> */