using AutoMapper;
using BigFun.API.Booking.Security.Domain.Models;
using BigFun.API.Booking.Security.Domain.Services.Communication;
using BigFun.API.Booking.Security.Resources;

namespace BigFun.API.Booking.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}