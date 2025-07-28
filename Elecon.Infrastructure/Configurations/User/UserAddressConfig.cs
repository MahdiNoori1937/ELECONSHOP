using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class UserAddressConfig:IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.HasKey(c=>c.Id);
        builder.Property(c=>c.CreateDate).IsRequired();
        builder.Property(c=>c.IsDelete).IsRequired();
        
        
        builder.Property(c=>c.Province).IsRequired().HasMaxLength(50);
        builder.Property(c=>c.City).IsRequired().HasMaxLength(50);
        builder.Property(c=>c.PostalCode).IsRequired().HasMaxLength(50);
        builder.Property(c=>c.Title).IsRequired().HasMaxLength(50);
        builder.Property(c=>c.FullAddress).IsRequired().HasMaxLength(200);
    }
}