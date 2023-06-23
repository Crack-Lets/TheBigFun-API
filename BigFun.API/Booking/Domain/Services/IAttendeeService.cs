using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IAttendeeService
{
    Task<IEnumerable<Attendee>> ListAsync();
    
    Task<IEnumerable<Event>> ListEventsByAttendeeAsync(int attendeeId);
    
    
    Task<AttendeeResponse> SaveAsync(Attendee attendee);
    Task<AttendeeResponse> UpdateAsync(int id, Attendee attendee);
    Task<AttendeeResponse> DeleteAsync(int id);

    Task<AttendeeResponse> AddEventToAttendee(int attendeeId, int eventId);
    
    Task<AttendeeResponse> AddPaymentToAttendee(int attendeeId, int paymentId);
}