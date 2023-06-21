namespace BigFun.API.Booking.Domain.Models;

public class Payment
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public Event events { get; set; }
}