using BigFun.API.Booking.Security.Authorization.Handlers.Interfaces;
using BigFun.API.Booking.Security.Authorization.Settings;
using BigFun.API.Booking.Security.Domain.Services;
using Microsoft.Extensions.Options;

namespace BigFun.API.Booking.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        if (userId != null)
        {
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        }

        await _next(context);
    }
}