using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Domain;

namespace ExpenseTracker.DbOperations;

public class ExpenseTrackDbContext:DbContext
{
    public ExpenseTrackDbContext(DbContextOptions<ExpenseTrackDbContext> options) : base (options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseTrackDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Demand> Demands { get; set; }
    public DbSet<Expense> Expenses{ get; set; }
    public DbSet<ExpenseDetail> ExpenseDetails{ get; set; }
    public DbSet<ExpenseManager> ExpenseManagers { get; set; }
    public DbSet<PaymentCategory> PaymentCategories { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<PersonnelAddress> PersonnelAddresses { get; set; }
    public DbSet<PersonnelPhone> PersonnelPhones { get; set; }
    public DbSet<Staff> Staffs { get; set; }
}
