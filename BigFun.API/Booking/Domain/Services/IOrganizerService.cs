using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IOrganizerService
{
    Task<IEnumerable<Organizer>> ListAsync();
    Task<OrganizerResponse> SaveAsync(Organizer organizer);
    Task<OrganizerResponse> UpdateAsync(int id,Organizer organizer);
    Task<OrganizerResponse> DeleteAsync(int id);

}