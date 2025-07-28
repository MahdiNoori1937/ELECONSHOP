using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class UserNotificationConfig:IEntityTypeConfiguration<UserNotification>
{
    public void Configure(EntityTypeBuilder<UserNotification> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.CreateDate).IsRequired();
        builder.Property(c=>c.IsDelete).IsRequired();
        
        
        builder.Property(c=>c.Message).IsRequired().HasMaxLength(200);
        builder.Property(c=>c.IsRead).IsRequired();

    }
}