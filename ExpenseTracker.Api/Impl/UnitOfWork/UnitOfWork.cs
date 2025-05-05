using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.DbOperations;
using ExpenseTracker.Api.Impl.GenericRepository;
using Serilog;
using ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository;
using ExpenseTracker.Api.Impl.GenericRepository.ExpenseRepository;

namespace ExpenseTracker.Api.Impl.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ExpenseTrackDbContext dbContext;

    public UnitOfWork(ExpenseTrackDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IExpenseRepository ExpenseRepository => new ExpenseRepository(dbContext);
    public IGenericRepository<ExpenseDetail> ExpenseDetailRepository => new GenericRepository<ExpenseDetail>(dbContext);
    public IGenericRepository<ExpenseManager> ExpenseManagerRepository => new GenericRepository<ExpenseManager>(dbContext);
    public IGenericRepository<PaymentCategory> PaymentCategoryRepository => new GenericRepository<PaymentCategory>(dbContext); 
    public IGenericRepository<PersonnelPhone> PersonnelPhoneRepository => new GenericRepository<PersonnelPhone>(dbContext);
    public IGenericRepository<PersonnelAddress> PersonnelAddressRepository => new GenericRepository<PersonnelAddress>(dbContext);
    IPersonnelRepository IUnitOfWork.PersonnelRepository => new PersonnelRepository(dbContext);

    public async Task Complete()
    {
        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while saving changes to the database.");
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    private bool _disposed = false;

}
