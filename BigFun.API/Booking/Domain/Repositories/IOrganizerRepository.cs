using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IOrganizerRepository
{
    Task<IEnumerable<Organizer>> ListAsync();
    Task AddAsync(Organizer organizer);
    
    Task<Organizer> FindByIdAsync(int id);
    Task<Organizer> FindByNameAsync(string name);
    Task<Organizer> FindByUserNameAsync(string userName);
    Task<Organizer> FindByEmailAsync(string email);

    //esto en IEventRepository y IPaymentRepository de Booking->Domain->Repositories respectivamente
   // Task<IEnumerable<Event>> FindByOrganizerIdAsync(int organizerId);
   //Task<IEnumerable<Payment>> FindByOrganizerIdAsync(int organizerId);

    void Update(Organizer organizer);
    void Remove(Organizer organizer);
}