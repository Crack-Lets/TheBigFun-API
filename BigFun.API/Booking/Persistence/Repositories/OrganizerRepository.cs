using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Shared.Persistence.Contexts;
using BigFun.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Booking.Persistence.Repositories;

public class OrganizerRepository: BaseRepository,IOrganizerRepository
{
    public OrganizerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Organizer>> ListAsync()
    {
        return await _context.Organizers.ToListAsync();
    }

    public async Task AddAsync(Organizer organizer)
    {
        await _context.Organizers.AddAsync(organizer);
    }

    public async Task<Organizer> FindByIdAsync(int id)
    {
        return await _context.Organizers.FindAsync(id);
    }

    public async Task<Organizer> FindByNameAsync(string name)
    {
        return await _context.Organizers.FindAsync(name);
    }

    public async Task<Organizer> FindByUserNameAsync(string userName)
    {
        return await _context.Organizers.FindAsync(userName);
    }

    public async Task<Organizer> FindByEmailAsync(string email)
    {
        return await _context.Organizers.FindAsync(email);
    }

    public void Update(Organizer organizer)
    {
        _context.Organizers.Update(organizer);
    }

    public void Remove(Organizer organizer)
    {
        _context.Organizers.Remove(organizer);
    }
}