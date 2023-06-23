namespace BigFun.API.Booking.Domain.Models;

public class Organizer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    // Relationships

    public IList<Event> EventsListByOrganizer { get; set; } = new List<Event>();
    //public IList<Payment> Payments { get; set; } = new List<Payment>();

    //esto en las clases de event y payment
    //public int OrganizerId { get; set; }
    //public Organizer Organizer { get; set; }
}