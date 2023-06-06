using System.Runtime.InteropServices.JavaScript;
using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Domain.Repositories;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> ListAsync();
    Task AddAsync(Payment user);
    Task<Payment> FindByIdAsync(int id);
    Task<Payment> FindByDateAsync(JSType.Date date);
    public bool ExistByDate(JSType.Date date);
    Payment FindById(int id);
    void Update(Payment payment);
    void Remove(Payment payment);
    Task<IEnumerable<Payment>> FindByOrganizerIdAsync(int organizerId);
}