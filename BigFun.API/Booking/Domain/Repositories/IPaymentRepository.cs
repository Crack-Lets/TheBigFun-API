using BigFun.API.Booking.Domain.Models;
using System.Runtime.InteropServices.JavaScript;


namespace BigFun.API.Booking.Domain.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> ListAsync();
    Task AddAsync(Payment payment);
    Task<Payment> FindByIdAsync(int id);
    Task<Payment> FindByDateAsync(JSType.Date date);
    void Update(Payment payment);
    void Remove(Payment payment);
    Task<IEnumerable<Payment>> ListByEventIdAsync(int eventId); 
    Task<IEnumerable<Payment>> ListByAttendeeIdAsync(int attendeeId); 
}