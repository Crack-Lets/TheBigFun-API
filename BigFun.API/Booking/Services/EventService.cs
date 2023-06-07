using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Domain.Services.Communication;
using BigFun.API.Shared.Domain.Repositories;

namespace BigFun.API.Booking.Services;

public class EventService : IEventService
{

    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Event>> ListAsync()
    {
        return await _eventRepository.ListAsync();
    }

    public async Task<EventResponse> SaveAsync(Event events)
    {
        try
        {
            await _eventRepository.AddSync(events);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(events);
        }
        catch (Exception e)
        {
            return new EventResponse($"An error occurred while saving the event: {e.Message}");
        }
    }

    public async Task<EventResponse> UpdateAsync(int id, Event events)
    {
        var existingEvent = await _eventRepository.FindByIdAsync(id);

        if (existingEvent == null)
            return new EventResponse("Event not found");

        existingEvent.Name = events.Name;

        try
        {
            _eventRepository.Update(existingEvent);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(existingEvent);
        }
        catch (Exception e)
        {
            return new EventResponse($"An error occurred while updating the event:{e.Message}");
        }
    }
    //AGREGADO DE ISA
    public async Task<IEnumerable<Event>> ListByOrganizerIdAsync(int organizerId)
    {
        return await _eventRepository.FindByOrganizerIdAsync(organizerId);
    }
//
    public async Task<EventResponse> DeleteAsync(int eventId)
    {
        var existingEvent = await _eventRepository.FindByIdAsync(eventId);
        if (existingEvent == null)
            return new EventResponse("Event not found");
        try
        {
            _eventRepository.Remove(existingEvent);
            await _unitOfWork.CompleteAsync();

            return new EventResponse(existingEvent);
        }
        catch (Exception e)
        {
            return new EventResponse($"An error occurred while deleting the event: {e.Message}");
        }
    }
}