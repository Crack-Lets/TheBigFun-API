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
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Event> Events { get; set;}

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


        
        builder.Entity<Organizer>().ToTable("Organizers");
        builder.Entity<Organizer>().HasKey(p => p.Id);
        builder.Entity<Organizer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Organizer>().Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Entity<Organizer>().Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Entity<Organizer>().Property(p => p.Email).IsRequired().HasMaxLength(255);

        builder.Entity<Organizer>().HasMany(p => p.EventsListByOrganizer)
            .WithOne(e => e.Organizer).HasForeignKey(z => z.OrganizerId);


        builder.Entity<Event>().ToTable("Events");
        builder.Entity<Event>().HasKey(p => p.Id);
        builder.Entity<Event>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Event>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Event>().Property(p => p.Address).IsRequired().HasMaxLength(240);
        builder.Entity<Event>().Property(p => p.Capacity).IsRequired();
        builder.Entity<Event>().Property(p => p.Image).IsRequired().HasMaxLength(500);
        builder.Entity<Event>().Property(p => p.Datetime).IsRequired();
        builder.Entity<Event>().Property(p => p.Cost).IsRequired();
        builder.Entity<Event>().Property(p => p.District).IsRequired();
        
        //FALTA Relations
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();




    }
}