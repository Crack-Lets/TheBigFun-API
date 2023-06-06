namespace BigFun.API.Booking.Resources;

public class OrganizerResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    //esto en EventResource y PaymentResource de booking->resources
    //public OrganizerResource Organizer { get; set; }
}