using Swashbuckle.AspNetCore.Annotations;

namespace BigFun.API.Booking.Resources;

[SwaggerSchema(Required = new []{"Id", "UserName"})]
public class AttendeeResource
{
    [SwaggerSchema("Attendee Identifier")]
    public int Id { get; set; }
    
    [SwaggerSchema("Attendee UserName")]
    public string UserName{get; set;}
    
    [SwaggerSchema("Attendee Name")]
    public string Name { get; set; }
    
    [SwaggerSchema("Attendee Email")]
    public string Email { get; set; }
}