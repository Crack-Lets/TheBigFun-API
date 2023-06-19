using System.ComponentModel.DataAnnotations;

namespace BigFun.API.Booking.Resources;

public class SaveAttendeeResource
{
    //[Required]  
    //[MaxLength(50)]
    public string UserName{get; set;}
    
    //[MaxLength(255)]
    public string Name { get; set; }
    
    //[MaxLength(255)]
    public string Email { get; set; } 
}