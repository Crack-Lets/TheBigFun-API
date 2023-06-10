using Swashbuckle.AspNetCore.Annotations;

namespace BigFun.API.Booking.Resources;

public class AttendeeResource
{
    public int Id { get; set; }
    public string UserName{get; set;}
    public string Name { get; set; }
    public string Email { get; set; }
}