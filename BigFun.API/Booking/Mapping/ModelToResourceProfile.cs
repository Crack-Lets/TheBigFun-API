using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Resources;

namespace BigFun.API.Booking.Mapping;

public class ModelToResourceProfile : Profile
{

    public ModelToResourceProfile()
    {

        CreateMap<Event, EventResource>();
        CreateMap<Organizer,OrganizerResource>();
        CreateMap<Attendee, AttendeeResource>();
        CreateMap<Payment, PaymentResource>();
    }
    
}