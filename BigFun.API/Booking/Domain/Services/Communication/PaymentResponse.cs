using BigFun.API.Booking.Domain.Models;
using BigFun.API.Shared.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services.Communication;

public class PaymentResponse : BaseResponse<Payment>
{
    public PaymentResponse(Payment resource) : base(resource)
    {
    }

    public PaymentResponse(string message) : base(message)
    {
    }
}