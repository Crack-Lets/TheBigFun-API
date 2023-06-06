using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IOrganizerRepository
{
    Task<IEnumerable<Organizer>> ListAsync();
    Task AddAsync(Organizer organizer);
    
    Task<Organizer> FindByIdAsync(int organizerId);
    Task<Organizer> FindByNameAsync(string name);
    Task<Organizer> FindByUserNameAsync(string userName);
    Task<Organizer> FindByEmailAsync(string email);

    Task<IEnumerable<Organizer>> FindByEventIdAsync(int eventId);
    Task<IEnumerable<Organizer>> FindByPaymentIdAsync(int paymentId);

    void Update(Organizer organizer);
    void Remove(Organizer organizer);
}