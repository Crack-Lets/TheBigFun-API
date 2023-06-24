using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IEventService
{
    Task<IEnumerable<Event>> ListAsync();
    Task<EventResponse> SaveAsync(Event events);
    Task<EventResponse> UpdateAsync(int id, Event events);
//AGREGADO DE ISA
    //Task<IEnumerable<Event>> ListByEventIdAsync(int organizerId);
    Task<EventResponse> DeleteAsync(int eventId);

    Task<EventResponse> AddPaymentToEvent(int eventId, int paymentId);

}