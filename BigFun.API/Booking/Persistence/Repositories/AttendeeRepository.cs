using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Shared.Persistence.Contexts;
using BigFun.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Booking.Persistence.Repositories;

public class AttendeeRepository : BaseRepository,IAttendeeRepository
{
    public AttendeeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Attendee>> ListAsync()
    {
        return await _context.Attendees.ToListAsync();
    }

    public async Task AddAsync(Attendee attendee)
    {
        await _context.Attendees.AddAsync(attendee);
    }

    public async Task<Attendee> FindByIdAsync(int id)
    {
        return await _context.Attendees.FindAsync(id);
    }

    public async Task<Attendee> FindByUserName(string userName)
    {
        return await _context.Attendees.FirstOrDefaultAsync(p => p.UserName == userName);
    }

    public async Task<Attendee> FindByEmail(string email)
    {
        return await _context.Attendees.FirstOrDefaultAsync(p => p.Email == email);
    }

    public void Update(Attendee attendee)
    {
        _context.Attendees.Update(attendee);
    }

    public void Remove(Attendee attendee)
    {
        _context.Attendees.Remove(attendee);
    }
}