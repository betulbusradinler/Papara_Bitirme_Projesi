using ExpenseTracker.Api.Domain;

namespace ExpenseTracker.Api.Impl.GenericRepository.ExpenseRepository;

public interface IExpenseRepository : IGenericRepository<Expense>
{
    Task<List<Expense>> GetAllExpensesWithExpenseDetail(params string[] includes);
    Task<List<Expense>> GetExpensesByPersonnelIdAsync(int StaffId, params string[] includes);
    Task<Expense> GetExpenseWithDetailByIdAsync(int id, params string[] includes);
    IQueryable<Expense> GetActiveExpensesByPersonnelId(int personnelId, params string[] includes);
}
