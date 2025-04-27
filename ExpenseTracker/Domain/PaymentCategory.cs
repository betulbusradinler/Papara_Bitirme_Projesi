using ExpenseTracker.Base.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Domain;

public class PaymentCategory:BaseEntity
{
  public string Name {get; set;} 
  public List<Expense> Expense {get; set;}
}

public class PaymentCategoryConfiguration : IEntityTypeConfiguration<PaymentCategory>
{
    public void Configure(EntityTypeBuilder<PaymentCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=> x.CreatedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

        builder.HasMany(x => x.Expense)
            .WithOne(x => x.PaymentCategory)
            .HasForeignKey(x => x.PaymentCategoryId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

    }
}
