using Microsoft.EntityFrameworkCore;

namespace Elecon.Infrastructure.ShopContext;

public class ShopContext:DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options):base(options)
    {
        
    }
}