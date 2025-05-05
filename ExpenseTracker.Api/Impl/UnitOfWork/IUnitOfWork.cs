using ExpenseTracker.Api.Domain;
using ExpenseTracker.Api.Impl.GenericRepository;
using ExpenseTracker.Api.Impl.GenericRepository.PersonnelRepository;
using ExpenseTracker.Api.Impl.GenericRepository.ExpenseRepository;

namespace ExpenseTracker.Api.Impl.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task Complete();
    IExpenseRepository ExpenseRepository { get; }
    IGenericRepository<ExpenseDetail> ExpenseDetailRepository { get; }
    IGenericRepository<ExpenseManager> ExpenseManagerRepository { get; }
    IGenericRepository<PaymentCategory> PaymentCategoryRepository { get; }
    IPersonnelRepository PersonnelRepository { get; }
    IGenericRepository<PersonnelAddress> PersonnelAddressRepository { get; }
    IGenericRepository<Payment> PaymentRepository { get; }
    IGenericRepository<PersonnelPhone> PersonnelPhoneRepository { get; }
}
