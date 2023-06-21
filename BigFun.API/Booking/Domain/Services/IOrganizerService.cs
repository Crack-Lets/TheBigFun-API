using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services.Communication;

namespace BigFun.API.Booking.Domain.Services;

public interface IOrganizerService
{
    Task<IEnumerable<Organizer>> ListAsync();
    //Task<Organizer> FindByIdAsyn(int id);
    Task<IEnumerable<Event>> ListEventsByOrganizerAsync(int organizerId);
    Task<OrganizerResponse> SaveAsync(Organizer organizer);
    Task<OrganizerResponse> UpdateAsync(int id,Organizer organizer);
    Task<OrganizerResponse> DeleteAsync(int id);
    
    Task<OrganizerResponse> AddEventToOrganizer(int organizerId, int eventId);

}