using System.Runtime.InteropServices.JavaScript;

namespace BigFun.API.Booking.Resources;

public class PaymentResource
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public DateTime Date { get; set; }
    //public string QrImg { get; set; }

    //PaymentResource Payment { get; set; }
    //public OrganizerResource Organizer { get; set; }
}