namespace BigFun.API.Booking.Resources;

public class SaveEventResource
{
    public string Name { get; set; }
    public string address { get; set; }
    public int Capacity { get; set; }
    public string Image { get; set; }
    public DateTime dateTime { get; set;}
    public int Cost { get; set; }
    public string District { get; set; }
    //public int OrganizerId { get; set; }
}