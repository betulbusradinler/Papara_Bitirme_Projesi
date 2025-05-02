using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;

public class PersonnelPassword:BaseEntity
{   
    public string Password {get; set;}
    public string Secret {get; set;}
    public int PersonnelId { get; set; }
    public Personnel Personnel { get; set; }  

}

public class PersonnelPasswordConfiguration : IEntityTypeConfiguration<PersonnelPassword>
{
    public void Configure(EntityTypeBuilder<PersonnelPassword> builder)
    {
        builder.HasKey(pp => pp.Id);

        builder.Property(x => x.CreatedDate).IsRequired(true);
        builder.Property(x => x.UpdatedDate).IsRequired(false);
        builder.Property(x => x.CreatedUser).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.UpdatedUser).IsRequired(false).HasMaxLength(250);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.PersonnelId).IsRequired(true);
        
    }
}
