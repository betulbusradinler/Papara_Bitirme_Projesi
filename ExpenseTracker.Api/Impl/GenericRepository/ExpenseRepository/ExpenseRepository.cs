using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Impl.GenericRepository.ExpenseRepository;

public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
{
    private readonly ExpenseTrackDbContext dbContext;

    public ExpenseRepository(ExpenseTrackDbContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Expense>> GetAllExpensesWithExpenseDetail(params string[] includes)
    {
        var query = dbContext.Set<Expense>()
            .Include(e => e.ExpenseDetail)
            .AsQueryable();

        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }

    public async Task<Expense> GetExpenseWithDetailByIdAsync(int id, params string[] includes)
    {
        var query = dbContext.Set<Expense>()
            .Include(e => e.ExpenseDetail)
            .AsQueryable();

        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.SingleOrDefaultAsync(e => e.Id == id);
    }


    public async Task<List<Expense>> GetExpensesByPersonnelIdAsync(int staffId, params string[] includes)
    {
        var query = dbContext.Set<Expense>()
            .Where(e => e.StaffId == staffId)
            .Include(e => e.ExpenseDetail)
            .AsQueryable();

        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }

    public IQueryable<Expense> GetActiveExpensesByPersonnelId(int personnelId, params string[] includes)
    {
        var query = dbContext.Set<Expense>()
          .Where(x => x.IsActive && x.StaffId == personnelId)
          .Include(e => e.ExpenseDetail)
          .AsQueryable();

        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return query;
    }

    
}