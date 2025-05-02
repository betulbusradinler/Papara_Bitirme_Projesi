using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;

public class PersonnelPhone:BaseEntity
{
    public int PersonnelId {get; set;}
    public Personnel Personnel {get; set;}
    public string CountryCode { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDefault { get; set; }
}

public class PersonnelPhoneConfiguration : IEntityTypeConfiguration<PersonnelPhone>
{
    public void Configure(EntityTypeBuilder<PersonnelPhone> builder)
    {
        builder.HasKey(pp => pp.Id);

        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.PersonnelId).IsRequired(true);
        builder.Property(pp => pp.CountryCode).IsRequired().HasMaxLength(3);
        builder.Property(pp => pp.PhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(pp => pp.IsDefault).IsRequired();
    }
}
