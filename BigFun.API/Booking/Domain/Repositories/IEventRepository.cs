using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<Event>> ListAsync();
    Task AddSync (Event events);
    Task<Event> FindByIdAsync(int id);
    //Task<IEnumerable<Event>> FindByOrganizerIdAsync(int organizerId);
    //Task<IEnumerable<Event>> ListByOrganizerIdAsync(int organizerId);
    void Update(Event events);
    void Remove(Event events);

}