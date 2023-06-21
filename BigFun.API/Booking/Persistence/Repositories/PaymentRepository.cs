using System.Data.Entity;
using System.Runtime.InteropServices.JavaScript;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Shared.Persistence.Contexts;
using BigFun.API.Shared.Persistence.Repositories;

namespace BigFun.API.Booking.Persistence.Repositories;

public class PaymentRepository : BaseRepository, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payment>> ListAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task<Payment> FindByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<Payment> FindByDateAsync(JSType.Date date)
    {
        return await _context.Payments.FindAsync(date);
        
    }
    
    public void Update(Payment payment)
    {
        _context.Payments.Update(payment);
    }

    public void Remove(Payment payment)
    {
        _context.Payments.Remove(payment);
    }


    public async Task<IEnumerable<Payment>> ListByEventIdAsync(int eventId)
    {

        var paymentsByEvent = _context.Payments.Where(p => p.EventId == eventId)
            .ToList();
        return paymentsByEvent;
    }
}