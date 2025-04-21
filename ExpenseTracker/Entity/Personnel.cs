using Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Entity;

public class Personnel:BaseEntity
{
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public DateTime OpenDate { get; set; }
    public virtual List<PersonnelPhone> PersonnelPhones { get; set; }
}

public class PersonnelConfiguration : IEntityTypeConfiguration<Personnel>
{
    public void Configure(EntityTypeBuilder<Personnel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        // her entity de burada bir kod tekrarı var
        builder.Property(x=> x.CreatedDate).IsRequired(true);
        builder.Property(x=> x.UpdatedDate).IsRequired(false);
        builder.Property(x=> x.CreatedPersonnel).IsRequired(true).HasMaxLength(250);
        builder.Property(x=> x.UpdatedPersonnel).IsRequired(false).HasMaxLength(250);
        builder.Property(x=> x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.OpenDate).IsRequired(true);


        builder.HasMany(x => x.PersonnelPhones)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

  /*      builder.HasMany(x => x.PersonnelAddresses)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Accounts)
            .WithOne(x => x.Personnel)
            .HasForeignKey(x => x.PersonnelId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

*/
    }
}
