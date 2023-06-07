using BigFun.API.Booking.Domain.Models;
using BigFun.API.Shared.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services.Communication;

public class OrganizerResponse:BaseResponse<Organizer>
{
    public OrganizerResponse(string message) : base(message)
    {
    }
    
    public OrganizerResponse(Organizer resource) : base(resource)
    {
    }
    
}