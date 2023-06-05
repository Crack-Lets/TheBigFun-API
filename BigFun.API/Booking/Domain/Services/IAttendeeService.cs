using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IAttendeeService
{
    Task<IEnumerable<Attendee>> ListAsync();
    Task<AttendeeResponse> SaveAsync(Attendee attendee);
    Task<AttendeeResponse> UpdateAsync(int id, Attendee attendee);
    Task<AttendeeResponse> DeleteAsync(int id);
}