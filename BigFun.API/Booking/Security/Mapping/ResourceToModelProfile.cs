using AutoMapper;
using BigFun.API.Booking.Security.Domain.Models;
using BigFun.API.Booking.Security.Domain.Services.Communication;
using Org.BouncyCastle.Asn1.X509;

namespace BigFun.API.Booking.Security.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
                ));
    }
}