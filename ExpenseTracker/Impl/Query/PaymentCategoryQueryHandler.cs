/* using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.DbOperations;
using ExpenseTracker.Domain;
using ExpenseTracker.Impl.Cqrs;
using ExpenseTracker.Base.ApiResponse;
using ExpenseTracker.Schema;
using ExpenseTracker.Impl.Command;

namespace ExpenseTracker.Impl.Query;

public class PaymentCategoryQueryHandler : 
IRequestHandler<GetAllPaymentCategoryQuery, ApiResponse<List<PaymentCategoryResponse>>>, 
IRequestHandler<GetPaymentCategoryByIdQuery, ApiResponse<PaymentCategoryResponse>>
{
    private readonly ExpenseTrackDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCategoryQueryHandler(ExpenseTrackDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentCategoryResponse>>> Handle(GetAllPaymentCategoryQuery request, CancellationToken cancellationToken)
    {
       
      var Entity = await dbContext.Set<PaymentCategory>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (Entity == null)
            return new ApiResponse("PaymentCategory not found");

        if (!Entity.IsActive)
            return new ApiResponse("PaymentCategory is not active");

        Entity.IsActive = false;

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse<List<PaymentCategoryResponse>>();
    }

    public async Task<ApiResponse<PaymentCategoryResponse>> Handle(GetPaymentCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        /* 
        var Entity = await dbContext.Set<PaymentCategory>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (Entity == null)
            return new ApiResponse("PaymentCategory not found");

        if (!Entity.IsActive)
            return new ApiResponse("PaymentCategory is not active");

        Entity.FirstName = request.PaymentCategory.FirstName;
        Entity.MiddleName = request.PaymentCategory.MiddleName;
        Entity.LastName = request.PaymentCategory.LastName;
        Entity.Email = request.PaymentCategory.Email;
        Entity.UpdatedDate = DateTime.Now;
        Entity.UpdatedUser = null;

        await dbContext.SaveChangesAsync(cancellationToken);
       //
        return new ApiResponse<PaymentCategoryResponse>();
    }

}
*/