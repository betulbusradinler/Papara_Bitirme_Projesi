using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;

//Masraf talebi için kategori bilgisi olmalı. Talep edilen ödeme kategorisi ödeme aracı ödeme yapılan konum ve fiş yada fatura vb dökümanlar sisteme yüklenebilmeli.
public class Expense:BaseEntity
{
  public int PaymentCategoryId {get; set;}
  public int StaffId {get; set;}
  public DemandState Demand {get; set;}
  public virtual Staff Staff {get; set;}
  public virtual PaymentCategory PaymentCategory {get; set;}
  public virtual ExpenseDetail ExpenseDetail {get; set;}
}
public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=> x.CreatedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.PaymentCategoryId).IsRequired(true);
        builder.Property(x => x.StaffId).IsRequired(true);
        
        builder.HasOne(x => x.ExpenseDetail)
          .WithOne(x => x.Expense)
          .HasForeignKey<ExpenseDetail>(x => x.ExpenseId)
          .IsRequired(true)
          .OnDelete(DeleteBehavior.Cascade);

    }    
}