/* using ExpenseTracker.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Domain;
// Burada talepler için gerekli alanı girmem gerekiyor
public class PersonnelRole:BaseEntity
{
    public string Name {get; set;}
    public List<Personnel> Personnels {get; set;}
}
public class PersonnelRoleConfiguration : IEntityTypeConfiguration<PersonnelRole>
{
   public void Configure(EntityTypeBuilder<PersonnelRole> builder)
   {
       builder.HasKey(x => x.Id);
       builder.Property(x=> x.CreatedDate).IsRequired(true);
       builder.Property(x=> x.UpdatedDate).IsRequired(false);
       builder.Property(x=> x.CreatedUser).IsRequired(true).HasMaxLength(250);
       builder.Property(x=> x.UpdatedUser).IsRequired(false).HasMaxLength(250);
       builder.Property(x=> x.Name).IsRequired(true).HasDefaultValue(50);

       builder.HasMany(x=>x.Personnels)
              .WithOne(x=>x.Role)
              .HasForeignKey(x=>x.RoleId).IsRequired(true).OnDelete(DeleteBehavior.Restrict);
   }
}
*/