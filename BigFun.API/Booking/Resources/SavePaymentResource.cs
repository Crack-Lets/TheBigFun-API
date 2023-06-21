using System.Runtime.InteropServices.JavaScript;

namespace BigFun.API.Booking.Resources;

public class SavePaymentResource
{
    public DateTime Date { get; set; }
    public string QrImg { get; set; }
    public int OrganizerId { get; set; }
}