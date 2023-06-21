using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Shared.Persistence.Contexts;
using BigFun.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Booking.Persistence.Repositories;

public class EventRepository :BaseRepository, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> ListAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<IEnumerable<Event>> ListByAttendeeAsync(int attendeeId)
    {
        var eventsByAttendee = _context.Attendees.Where(u => u.Id == attendeeId)
            .SelectMany(u => u.EventsListByAttendee).ToList();

        return eventsByAttendee;
    }

    public async Task<IEnumerable<Event>> ListByOrganizerAsync(int organizerId)
    {
        var eventsByOrganizer = _context.Events.Where(u => u.OrganizerId == organizerId)
            .ToList();

        return eventsByOrganizer;
    }
    
    

    public async Task AddSync(Event events)
    {
        await _context.Events.AddAsync(events);
    }
    
    
    
    

    public async Task<Event> FindByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    /*public async Task<IEnumerable<Event>> ListByEventIdAsync(int organizerId)
    {
        return await _context.Events
            .Where(p => p.OrganizerId == organizerId)
            .Include(p => p.Organizer)
            .ToListAsync();
    }
//AGREGADO DE ISABELA
    public async Task<IEnumerable<Event>> ListByEventIdAsync(int organizerId)
    {
        return await _context.Events.Where(p => p.OrganizerId == organizerId)
            .Include(p => p.Organizer).ToListAsync();
    }*/
//
    public void Update(Event events)
    {
        _context.Events.Update(events);
    }

    public void Remove(Event events)
    {
        _context.Events.Remove(events);
    }
}