using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Resources;

namespace BigFun.API.Booking.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveAttendeeResource, Attendee>();
        CreateMap<SaveOrganizerResource,Organizer>();
        CreateMap<SaveEventResource, Event>();
        CreateMap<SavePaymentResource, Payment>();
    }
}