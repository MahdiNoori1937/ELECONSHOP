using ELECON.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Elecon.Infrastructure.ShopContext;

public class ShopContext:DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options):base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<UserSecurity> UserSecurities { get; set; }
    public DbSet<UserLoginHistory> UserLoginHistories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);


        foreach (IMutableEntityType EntityType in modelBuilder.Model.GetEntityTypes())
        {
            EntityType.GetForeignKeys()
                .Where(c=>!c.IsOwnership && c.DeleteBehavior==DeleteBehavior.Cascade)
                .ToList()
                .ForEach(c=>c.DeleteBehavior=DeleteBehavior.Restrict);
        }
    }
}