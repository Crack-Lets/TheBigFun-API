namespace BigFun.API.Booking.Domain.Models;

public class Attendee
{
    public int Id { get; set; }
    public string UserName{get; set;}
    public string Name { get; set; }
    public string Email { get; set; }

    // Relationships
    public IList<Event> EventsListByAttendee { get; set; } = new List<Event>();
    //public IList<Payment> Payments { get; set; } = new List<Payment>();

}