using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Resources;

public class EventResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public string Image { get; set; }
    public DateTime Datetime { get; set;}
    public int Cost { get; set; }
    public string District { get; set; }
   //AGREGADO DE ISA
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }
    
}