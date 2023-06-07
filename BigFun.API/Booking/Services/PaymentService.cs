using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Domain.Services.Communication;
using BigFun.API.Booking.Persistence.Repositories;
using BigFun.API.Shared.Domain.Repositories;

namespace BigFun.API.Booking.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
    }
    

    public async Task<IEnumerable<Payment>> ListAsync()
    {
        return await _paymentRepository.ListAsync();
    }

    public async Task<PaymentResponse> SaveAsync(Payment payment)
    {
        try
        {
            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(payment);
        }
        catch (Exception e)
        {
            return new PaymentResponse($"An error occurred while saving the payment: {e.Message}");
        }
    }

    public async Task<PaymentResponse> UpdateAsync(int id, Payment payment)
    {
        var existingPayment = await _paymentRepository.FindByIdAsync(id);
        if (existingPayment == null)
            return new PaymentResponse("Payment not found");
        existingPayment.Date = payment.Date;
        try
        {
            _paymentRepository.Update(existingPayment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(existingPayment);
        }
        catch (Exception e)
        {
            return new PaymentResponse($"An error occurred while updating the payment:{e.Message}");
        }
    }

    public async Task<PaymentResponse> DeleteAsync(int id)
    {
        var existingPayment = await _paymentRepository.FindByIdAsync(id);
        if (existingPayment == null)
            return new PaymentResponse("Payment not found");
        try
        {
            _paymentRepository.Remove(existingPayment);
            await _unitOfWork.CompleteAsync();
            return new PaymentResponse(existingPayment);
        }
        catch(Exception e)
        {
            return new PaymentResponse($"An error occurred while deleting the category{e.Message}");
        }
    }
    public async Task<IEnumerable<Payment>> ListByOrganizerIdAsync(int organizerId)
    {
        return await _paymentRepository.FindByOrganizerIdAsync(organizerId);
    }
}