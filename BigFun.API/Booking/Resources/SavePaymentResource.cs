using System.Runtime.InteropServices.JavaScript;

namespace BigFun.API.Booking.Resources;

public class SavePaymentResource
{
    public JSType.Date Date { get; set; }
    public string QrImg { get; set; }
    public int OrganizerId { get; set; }
}