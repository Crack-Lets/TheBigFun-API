using System.Runtime.InteropServices.JavaScript;

namespace BigFun.API.Booking.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public JSType.Date Date { get; set; }
    public string QrImg { get; set; }
    public int OrganizerId { get; set; }
    public Organizer Organizer { get; set; }
}