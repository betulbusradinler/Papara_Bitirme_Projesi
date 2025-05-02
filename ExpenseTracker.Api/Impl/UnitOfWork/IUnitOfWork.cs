using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.GenericRepository;

namespace ExpenseTracker.Api.Impl.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task Complete();
    IGenericRepository<Demand> DemandRepository { get; }
    IGenericRepository<Expense> ExpenseRepository { get; }
    IGenericRepository<ExpenseDetail> ExpenseDetailRepository { get; }
    IGenericRepository<ExpenseManager> ExpenseManagerRepository { get; }
    IGenericRepository<PaymentCategory> PaymentCategoryRepository { get; }
    IGenericRepository<Personnel> PersonnelRepository { get; }
    IGenericRepository<PersonnelAddress> PersonnelAddressRepository { get; }
    IGenericRepository<PersonnelPhone> PersonnelPhoneRepository { get; }
}
