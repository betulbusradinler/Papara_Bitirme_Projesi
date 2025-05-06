using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;

public class Personnel:BaseEntity
{
    public string Role {get; set;}
    public string UserName {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string Iban { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public virtual PersonnelPassword PersonnelPassword { get; set; }
    public virtual List<PersonnelPhone> PersonnelPhones { get; set; }
    public virtual List<PersonnelAddress> PersonnelAddresses { get; set; }
}

public class PersonnelConfiguration : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=> x.CreatedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.OpenDate).IsRequired(true);


        builder.HasMany(x => x.PersonnelPhones)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.PersonnelAddresses)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PersonnelPassword)
            .WithOne(x => x.Personnel)
            .HasForeignKey<PersonnelPassword>(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
    }
}
