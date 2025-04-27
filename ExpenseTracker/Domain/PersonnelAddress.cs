using ExpenseTracker.Base.Domain;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Domain;
public class PersonnelAddress:BaseEntity
{
    public int PersonnelId {get; set;}
    public Personnel Personnel {get; set;}
     public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

public class PersonnelAddressConfiguration : IEntityTypeConfiguration<PersonnelAddress>
{
    public void Configure(EntityTypeBuilder<PersonnelAddress> builder)
    {
        builder.HasKey(pa => pa.Id);

        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.PersonnelId).IsRequired(true);
        builder.Property(pa => pa.CountryCode).IsRequired().HasMaxLength(3);
        builder.Property(pa => pa.City).IsRequired().HasMaxLength(100);
        builder.Property(pa => pa.District).IsRequired().HasMaxLength(100);
        builder.Property(pa => pa.Street).IsRequired().HasMaxLength(100);
        builder.Property(pa => pa.ZipCode).IsRequired().HasMaxLength(10);
        builder.Property(pa => pa.IsDefault).IsRequired();
    }
}
