using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.GenericRepository;
using ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository;

namespace ExpenseTracker.Api.Impl.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task Complete();
    IGenericRepository<Demand> DemandRepository { get; }
    IGenericRepository<Expense> ExpenseRepository { get; }
    IGenericRepository<ExpenseDetail> ExpenseDetailRepository { get; }
    IGenericRepository<ExpenseManager> ExpenseManagerRepository { get; }
    IGenericRepository<PaymentCategory> PaymentCategoryRepository { get; }
    IPersonnelRepository PersonnelRepository { get; }
    IGenericRepository<PersonnelAddress> PersonnelAddressRepository { get; }
    IGenericRepository<PersonnelPhone> PersonnelPhoneRepository { get; }
}
