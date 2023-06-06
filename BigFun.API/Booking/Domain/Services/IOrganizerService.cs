using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IOrganizerService
{
    Task<IEnumerable<Organizer>> ListAsync();
    Task<IEnumerable<Organizer>> ListByOrganizerIdAsync(int organizerId);
    Task<OrganizerResponse> SaveAsync(Organizer organizer);
    Task<OrganizerResponse> UpdateAsync(int organizerId,Organizer organizer);
    Task<OrganizerResponse> DeleteAsync(int organizerId);

}