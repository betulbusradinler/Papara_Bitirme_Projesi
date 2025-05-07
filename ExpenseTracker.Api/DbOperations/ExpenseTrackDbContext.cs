using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Api.Domain;
using ExpenseTracker.Base;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection;

namespace ExpenseTracker.Api.DbOperations;

public class ExpenseTrackDbContext:DbContext
{
    private readonly IAppSession appSession;

    public ExpenseTrackDbContext(DbContextOptions<ExpenseTrackDbContext> options, IServiceProvider serviceProvider) : base(options)
    {
        this.appSession = serviceProvider.GetService<IAppSession>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpenseTrackDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Personnel>().HasData(
           new Personnel
           {
               Id = -1,
               Role = "Admin",
               UserName = "admin",
               FirstName = "Admin",
               LastName = "User",
               Email = "admin@admin.com",
               Iban = "TR1234567890",
               OpenDate = DateTime.Now,
               CreatedDate = DateTime.Now,
               CreatedUser = "System",
               IsActive = true
           },
           new Personnel
           {
               Id = -2,
               Role = "Personnel",
               UserName = "personel",
               FirstName = "John",
               LastName = "Doe",
               Email = "personel@personel.com",
               Iban = "TR0987654321",
               OpenDate = DateTime.Now,
               CreatedDate = DateTime.Now,
               CreatedUser = "System",
               IsActive = true
           }
       );

        modelBuilder.Entity<PersonnelPassword>().HasData(
             new PersonnelPassword
             {
                 Id = -1,
                 PersonnelId = -1,
                 Password = "lzrYokbJ5sItqIb9t1mk5VRKmC6SYJtrKDzpwFjvXs2pbA+fwHqydW8/IDnIAQMnM5k5GFgCmVhxJ4CJ8vF08Q==", //test123
                 Secret = "LURfZOB6CtJ3gspXcsj3tx9C3YZRgKTVtdPlubEkhATaRx9iF0oZ25V9aRbD5B63M8WDdV2T/1X9ZVL2K9PKew==",
                 CreatedDate = DateTime.UtcNow,
                 CreatedUser = "system",
                 IsActive = true,
             }
        );

        modelBuilder.Entity<PersonnelPassword>().HasData(
             new PersonnelPassword
             {
                 Id = -2,
                 PersonnelId = -2,
                 Password = "lzrYokbJ5sItqIb9t1mk5VRKmC6SYJtrKDzpwFjvXs2pbA+fwHqydW8/IDnIAQMnM5k5GFgCmVhxJ4CJ8vF08Q==", //test123
                 Secret = "LURfZOB6CtJ3gspXcsj3tx9C3YZRgKTVtdPlubEkhATaRx9iF0oZ25V9aRbD5B63M8WDdV2T/1X9ZVL2K9PKew==",
                 CreatedDate = DateTime.UtcNow,
                 CreatedUser = "system",
                 IsActive = true,
             }
        );

    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entyList = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity
         && (e.State == EntityState.Deleted || e.State == EntityState.Added || e.State == EntityState.Modified));

        var auditLogs = new List<AuditLog>();

        foreach (var entry in entyList)
        {
            var baseEntity = (BaseEntity)entry.Entity;
            var properties = entry.Properties.ToList();
            var changedProperties = properties.Where(p => p.IsModified).ToList();
            var changedValues = changedProperties.ToDictionary(p => p.Metadata.Name, p => p.CurrentValue);
            var originalValues = properties.ToDictionary(p => p.Metadata.Name, p => p.OriginalValue);
            var changedValuesString = JsonConvert.SerializeObject(changedValues.Select(kvp => new { Key = kvp.Key, Value = kvp.Value }));
            var originalValuesString = JsonConvert.SerializeObject(originalValues.Select(kvp => new { Key = kvp.Key, Value = kvp.Value }));

            var auditLog = new AuditLog
            {
                EntityName = entry.Entity.GetType().Name,
                EntityId = baseEntity.Id.ToString(),
                Action = entry.State.ToString(),
                Timestamp = DateTime.Now,
                UserName = appSession?.UserName ?? "anonymous",
                ChangedValues = changedValuesString,
                OriginalValues = originalValuesString,
            };

            if (entry.State == EntityState.Added)
            {
                baseEntity.CreatedDate = DateTime.Now;
                baseEntity.CreatedUser = appSession?.UserName ?? "anonymous";
                baseEntity.IsActive = true;
            }
            else if (entry.State == EntityState.Modified)
            {
                baseEntity.UpdatedDate = DateTime.Now;
                baseEntity.UpdatedUser = appSession?.UserName ?? "anonymous";
            }
            else if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                baseEntity.IsActive = false;
                baseEntity.UpdatedDate = DateTime.Now;
                baseEntity.UpdatedUser = appSession?.UserName ?? "anonymous";
            }

            auditLogs.Add(auditLog);
        }

        if (auditLogs.Any())
        {
            Set<AuditLog>().AddRange(auditLogs);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Expense> Expenses{ get; set; }
    public DbSet<ExpenseDetail> ExpenseDetails{ get; set; }
    public DbSet<ExpenseManager> ExpenseManagers { get; set; }
    public DbSet<PaymentCategory> PaymentCategories { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<PersonnelPassword> PersonnelPasswords { get; set; }
    public DbSet<PersonnelAddress> PersonnelAddresses { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Payment> Payments { get; set; }
}
