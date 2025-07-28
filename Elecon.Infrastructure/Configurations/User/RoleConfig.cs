using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class RoleConfig:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.CreateDate).IsRequired();
        builder.Property(c=>c.IsDelete).IsRequired();
        
        
        builder.Property(c=>c.Title).IsRequired().HasMaxLength(50).IsUnicode();
    }
}