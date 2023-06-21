using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Domain.Services.Communication;
using BigFun.API.Shared.Domain.Repositories;

namespace BigFun.API.Booking.Services;

public class OrganizerService: IOrganizerService
{
    private readonly IOrganizerRepository _organizerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventRepository _eventRepository;


    public OrganizerService(IOrganizerRepository organizerRepository, IUnitOfWork unitOfWork,IEventRepository eventRepository)
    {
        _organizerRepository = organizerRepository;
        _unitOfWork = unitOfWork;
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Organizer>> ListAsync()
    {
        return await _organizerRepository.ListAsync();
    }

    /*public async Task<Organizer> FindByIdAsyn(int id)
    {
        return await 
    }*/
    
    public async Task<IEnumerable<Event>> ListEventsByOrganizerAsync(int organizerId)
    {
        return await _eventRepository.ListByOrganizerAsync(organizerId);
    }

    public async Task<OrganizerResponse> SaveAsync(Organizer organizer)
    {
        try
        {
            await _organizerRepository.AddAsync(organizer);
            await _unitOfWork.CompleteAsync();
            return new OrganizerResponse(organizer);
        }
        catch (Exception e)
        {
            return new OrganizerResponse($"An error occurred while saving the organizer: {e.Message}");
        }
    }

    public async Task<OrganizerResponse> UpdateAsync(int id, Organizer organizer)
    {
        var existingOrganizer = await _organizerRepository.FindByIdAsync(id);

        if (existingOrganizer == null)
            return new OrganizerResponse("Organizer not found.");

        existingOrganizer.Name = organizer.Name;

        try
        {
            _organizerRepository.Update(existingOrganizer);
            await _unitOfWork.CompleteAsync();

            return new OrganizerResponse(existingOrganizer);
        }
        catch (Exception e)
        {
            return new OrganizerResponse($"An error occurred while updating the organizer: {e.Message}");
        }    
    }

    public async Task<OrganizerResponse> DeleteAsync(int id)
    {
        var existingOrganizer = await _organizerRepository.FindByIdAsync(id);

        if (existingOrganizer == null)
            return new OrganizerResponse("Organizer not found.");

        try
        {
            _organizerRepository.Remove(existingOrganizer);
            await _unitOfWork.CompleteAsync();

            return new OrganizerResponse(existingOrganizer);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new OrganizerResponse($"An error occurred while deleting the organizer: {e.Message}");
        }    
    }

    public async Task<OrganizerResponse> AddEventToOrganizer(int organizerId, int eventId)
    {
        var organizer = await _organizerRepository.FindByIdAsync(organizerId);
        var eventt = await _eventRepository.FindByIdAsync(eventId);

        if (organizer == null || eventt == null)
        {
            return new OrganizerResponse("one of the ids doesn't exist");
        }

        try
        {
            organizer.EventsListByOrganizer.Add(eventt);
            await _unitOfWork.CompleteAsync();
            return new OrganizerResponse(organizer);
        }
        catch (Exception e)
        {
            return new OrganizerResponse($"An error occurred while adding event to attendee : {e.Message}");
        }
    }
}