using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        //  ENTITIES
        
        
        
        
        
    }
}