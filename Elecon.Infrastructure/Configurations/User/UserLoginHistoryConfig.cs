using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class UserLoginHistoryConfig:IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.HasKey(c=>c.Id);
        
        
        builder.Property(c=>c.IpAddress).IsRequired().HasMaxLength(50);
        builder.Property(c=>c.IsSuccess).IsRequired();
        builder.Property(c=>c.LogTime).IsRequired();
    }
}