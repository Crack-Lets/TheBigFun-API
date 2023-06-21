using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IAttendeeRepository
{
    Task<IEnumerable<Attendee>> ListAsync();
    Task AddAsync(Attendee attendee);
    Task<Attendee> FindByIdAsync(int id);
    void Update(Attendee attendee);
    void Remove(Attendee attendee);
}