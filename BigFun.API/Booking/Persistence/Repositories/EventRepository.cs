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

    public async Task AddSync(Event events)
    {
        await _context.Events.AddAsync(events);
    }

    public async Task<Event> FindByIdAsync(int id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task<IEnumerable<Event>> FindByOrganizerIdAsync(int organizerId)
    {
        
        //esto falta
        throw new NotImplementedException();
    }

    public void Update(Event events)
    {
        _context.Events.Update(events);
    }

    public void Remove(Event events)
    {
        _context.Events.Remove(events);
    }
}