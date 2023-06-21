namespace BigFun.API.Booking.Domain.Models;
using System.Runtime.InteropServices.JavaScript;


public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int EventId { get; set; }
    public Event Events { get; set; }
}