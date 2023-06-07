using System.Runtime.InteropServices.JavaScript;

namespace BigFun.API.Booking.Resources;

public class PaymentResource
{
    public int Id { get; set; }
    public JSType.Date Date { get; set; }
    public String QrImg { get; set; }

    PaymentResource Payment { get; set; }
    public OrganizerResource Organizer { get; set; }
}