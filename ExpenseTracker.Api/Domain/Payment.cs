using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExpenseTracker.Api.Domain;

public class Payment : BaseEntity
{
  public int ExpenseId { get; set; }
  public DateTime PaymentDate { get; set; }
  public Expense Expense { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
  public void Configure(EntityTypeBuilder<Payment> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(x => x.CreatedDate).IsRequired(true);
    builder.Property(x => x.UpdatedDate).IsRequired(false);
    builder.Property(x => x.CreatedUser).IsRequired(true).HasMaxLength(250);
    builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
    builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

    builder.Property(x => x.ExpenseId).IsRequired(true);
    builder.Property(x => x.PaymentDate).IsRequired(true);

  }
}
