using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;
//Masraf talebi için kategori bilgisi olmalı. Talep edilen ödeme kategorisi ödeme aracı ödeme yapılan konum ve fiş yada fatura vb dökümanlar sisteme yüklenebilmeli.
public class ExpenseDetail:BaseEntity
{
  public int ExpenseId {get; set;}
  public decimal Amount {get; set;}
  public string? Description {get; set;}
  public string PaymentPoint {get; set;} 
  public string PaymentInstrument {get; set;}
  public string Receipt {get; set;}  
  public Expense Expense {get; set;}
}
public class ExpenseDetailConfiguration : IEntityTypeConfiguration<ExpenseDetail>
{
    public void Configure(EntityTypeBuilder<ExpenseDetail> builder)
    {
        builder.HasKey(x => x.Id);
        // her Entity de burada bir kod tekrarı var
        builder.Property(x=> x.CreatedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(250);
        builder.Property(x => x.PaymentPoint).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PaymentInstrument).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Receipt).IsRequired().HasMaxLength(100);

    }
}