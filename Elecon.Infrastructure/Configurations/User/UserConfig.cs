using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elecon.Infrastructure.Configurations.User;

public class UserConfig:IEntityTypeConfiguration<ELECON.Domain.Entities.User.User>
{
    public void Configure(EntityTypeBuilder<ELECON.Domain.Entities.User.User> builder)
    {
       builder.HasKey(c=>c.Id);
       builder.Property(c=>c.CreateDate).IsRequired();
       builder.Property(c=>c.IsDelete).IsRequired();
       
       
       builder.Property(c=>c.FristName).IsRequired(false).HasMaxLength(50);
       builder.Property(c=>c.LastName).IsRequired(false).HasMaxLength(50);
       builder.Property(c=>c.Pasword).IsRequired(false).HasMaxLength(300);
       builder.Property(c=>c.Email).IsRequired(false).HasMaxLength(100);
       builder.Property(c=>c.UserProfileImage).IsRequired(false).HasMaxLength(200);
       builder.Property(c=>c.UserStatus).IsRequired().HasMaxLength(15);
       builder.Property(c=>c.PhoneNumber).IsRequired().HasMaxLength(50);


       #region Relations
       builder.HasOne(c=>c.Role).WithMany(c=>c.Users).HasForeignKey(c=>c.RoleId);
       
       builder.HasMany(c=>c.LoginHistories).WithOne(c=>c.User).HasForeignKey(c=>c.UserId);
       builder.HasMany(c=>c.UserAddresses).WithOne(c=>c.User).HasForeignKey(c=>c.UserId);
       builder.HasMany(c=>c.UserNotifications).WithOne(c=>c.User).HasForeignKey(c=>c.UserId);
  
       
       builder.HasOne<UserSecurity>(c=>c.UserSecurity).WithOne(c=>c.User).HasForeignKey<UserSecurity>(c=>c.UserId);

       #endregion

    }
}