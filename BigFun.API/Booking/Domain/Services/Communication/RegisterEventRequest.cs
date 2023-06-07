using System.ComponentModel.DataAnnotations;

namespace BigFun.API.Booking.Domain.Services.Communication;

public class RegisterEventRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string address { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public string Image { get; set; }
    [Required]
    public DateTime dateTime { get; set;}
    [Required]
    public int Cost { get; set; }
    [Required]
    public string District { get; set; }
}