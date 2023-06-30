using BigFun.API.Booking.Domain.Models;
using BigFun.API.Shared.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services.Communication;

public class EventResponse : BaseResponse<Event>
{
    public EventResponse(Event resource) : base(resource)
    {
    }

    public EventResponse(string message) : base(message)
    {
    }
}