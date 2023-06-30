using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IAttendeeRepository
{
    Task<IEnumerable<Attendee>> ListAsync();
    Task AddAsync(Attendee attendee);
    Task<Attendee> FindByIdAsync(int id);
    
    Task<Attendee> FindByUserName(string userName);
    
    Task<Attendee> FindByEmail(string email);
    
    void Update(Attendee attendee);
    void Remove(Attendee attendee);
}