using BigFun.API.Booking.Security.Domain.Models;

namespace BigFun.API.Booking.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}