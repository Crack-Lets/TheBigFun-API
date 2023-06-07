using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IEventService
{
    Task<IEnumerable<Event>> ListAsync();
    Task<EventResponse> SaveAsync(Event events);
    Task<EventResponse> UpdateAsync(int id, Event events);
    Task<EventResponse> DeleteAsync(int eventId);
    
}