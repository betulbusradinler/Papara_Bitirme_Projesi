using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.Domain;

[Table("AuditLog", Schema = "dbo")]
public class AuditLog
{
    public long Id { get; set; }
    public string EntityName { get; set; }
    public string EntityId { get; set; }
    public string Action { get; set; }
    public DateTime? Timestamp { get; set; }
    public string UserName { get; set; }
    public string ChangedValues { get; set; }
    public string OriginalValues { get; set; }
}

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.EntityName).IsRequired(false).HasMaxLength(2000);
        builder.Property(x => x.EntityId).IsRequired(false).HasMaxLength(2000);
        builder.Property(x => x.Action).IsRequired(false).HasMaxLength(2000);
        builder.Property(x => x.Timestamp).IsRequired(false);
        builder.Property(x => x.UserName).IsRequired(false).HasMaxLength(2000);

        builder.Property(x => x.ChangedValues).IsRequired(false);
        builder.Property(x => x.OriginalValues).IsRequired(false);
    }
}