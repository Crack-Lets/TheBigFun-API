using System.Runtime.InteropServices.JavaScript;
using BigFun.API.Booking.Domain.Models;

namespace BigFun.API.Booking.Resources;

public class PaymentResource
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int EventId { get; set; }
    public EventResource Event { get; set; }
    public int AttendeeId { get; set; }
    public AttendeeResource Attendee { get; set; }
    
    //public string QrImg { get; set; }

    //PaymentResource Payment { get; set; }
    //public OrganizerResource Organizer { get; set; }
}