using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class UserSecurityConfig:IEntityTypeConfiguration<UserSecurity>
{
    public void Configure(EntityTypeBuilder<UserSecurity> builder)
    {
        builder.HasKey(c=>c.Id);
        
        
        builder.Property(c=>c.IsLockedOut).IsRequired(false);
        builder.Property(c=>c.LastFailedLogin).IsRequired(false);
        builder.Property(c=>c.LockoutEnd).IsRequired(false);
        builder.Property(c=>c.FailedLoginAttempts).IsRequired();
      
    }
}