using BigFun.API.Booking.Domain.Models;
using BigFun.API.Shared.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services.Communication;

public class AttendeeResponse : BaseResponse<Attendee>
{
    public AttendeeResponse(Attendee resource) : base(resource)
    {
    }

    public AttendeeResponse(string message) : base(message)
    {
    }
}