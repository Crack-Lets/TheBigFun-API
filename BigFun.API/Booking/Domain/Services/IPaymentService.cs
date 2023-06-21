using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IPaymentService
{
    Task<IEnumerable<Payment>> ListAsync();
    Task<PaymentResponse> SaveAsync(Payment payment);
    Task<PaymentResponse> UpdateAsync(int id, Payment payment);
    Task<PaymentResponse> DeleteAsync(int id);
    Task<IEnumerable<Payment>> ListByEventIdAsync(int eventId);
    

}