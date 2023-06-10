namespace BigFun.API.Booking.Domain.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public string Image { get; set; }
    public DateTime Datetime { get; set;}
    public int Cost { get; set; }
    public string District { get; set; }
    
    //Relationships


    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }


    public IList<Event> Events { get; set; } = new List<Event>();
}