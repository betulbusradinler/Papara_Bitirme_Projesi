using AutoMapper;
using MediatR;
using ExpenseTracker.Base;
using ExpenseTracker.Schema;
using ExpenseTracker.Api.Impl.Cqrs;
using ExpenseTracker.Api.Impl.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Impl.Query;

public class ExpenseQueryHandler :
IRequestHandler<GetAllExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
IRequestHandler<GetAllPersonnelExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
IRequestHandler<GetFilteredExpensesQuery, ApiResponse<List<ExpenseResponse>>>
{
      private readonly IUnitOfWork unitOfWork;
      private readonly IAppSession appSession;
      private readonly IMapper mapper;

      private const string ExpenseNotFoundMessage = "Harcama bulunamadı veya aktif değil";
      public ExpenseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IAppSession appSession)
      {
            this.appSession = appSession;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
      }

      public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpenseQuery request, CancellationToken cancellationToken)
      {
            var entity = await unitOfWork.ExpenseRepository.GetAllExpensesWithExpenseDetail();
            entity.RemoveAll(p => p.IsActive == false);

            if (entity.Count <= 0)
                  return new ApiResponse<List<ExpenseResponse>>(ExpenseNotFoundMessage, 400);

            var mapped = mapper.Map<List<ExpenseResponse>>(entity);
            return new ApiResponse<List<ExpenseResponse>>(mapped);
      }

      public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllPersonnelExpenseQuery request, CancellationToken cancellationToken)
      {
            int convertPersonnelId = Convert.ToInt32(appSession.PersonnelId);
            var entity = await unitOfWork.ExpenseRepository.GetAllExpensesByPersonnelIdAsync(convertPersonnelId);

            entity.RemoveAll(p => p.IsActive == false);
            if (entity.Count <= 0)
                  return new ApiResponse<List<ExpenseResponse>>(ExpenseNotFoundMessage, 400);

            var mapped = mapper.Map<List<ExpenseResponse>>(entity);
            return new ApiResponse<List<ExpenseResponse>>(mapped);
      }

      public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
      {
            var entity = await unitOfWork.ExpenseRepository.GetByIdAsync(request.Id);
            if (entity == null || entity.IsActive == false)
                  return new ApiResponse<ExpenseResponse>(ExpenseNotFoundMessage, 400);

            var mapped = mapper.Map<ExpenseResponse>(entity);
            return new ApiResponse<ExpenseResponse>(mapped);
      }

      public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetFilteredExpensesQuery request, CancellationToken cancellationToken)
      {
            int personnelId = Convert.ToInt32(appSession.PersonnelId);

            var query = unitOfWork.ExpenseRepository.GetActiveExpensesByPersonnelId(personnelId);

            if (request.Filter.PaymentCategoryId.HasValue)
                  query = query.Where(x => x.PaymentCategoryId == request.Filter.PaymentCategoryId);

            if (request.Filter.DemandState.HasValue)
                  query = query.Where(x => (int)x.Demand == request.Filter.DemandState.Value);

            if (request.Filter.StartDate.HasValue)
                  query = query.Where(x => x.CreatedDate >= request.Filter.StartDate.Value);

            if (request.Filter.EndDate.HasValue)
                  query = query.Where(x => x.CreatedDate <= request.Filter.EndDate.Value.Date.AddDays(1).AddTicks(-1));

            var list = await query.ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<ExpenseResponse>>(list);
            return new ApiResponse<List<ExpenseResponse>>(mapped);
      }
}