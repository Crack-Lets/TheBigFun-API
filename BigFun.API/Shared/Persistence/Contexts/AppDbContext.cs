using BigFun.API.Booking.Domain.Models;
using BigFun.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{


    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    
    public DbSet<Attendee> Attendees { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        //  ENTITIES
        builder.Entity<Attendee>().ToTable("Attendees");
        builder.Entity<Attendee>().HasKey(p => p.Id);
        builder.Entity<Attendee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Attendee>().Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Entity<Attendee>().Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Entity<Attendee>().Property(p => p.Email).IsRequired().HasMaxLength(255);

        //builder.Entity<Attendee>



        
        builder.UseSnakeCaseNamingConvention();

    }
}