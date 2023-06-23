using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Domain.Services.Communication;
using BigFun.API.Shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Services;

public class EventService : IEventService
{

    private readonly IEventRepository _eventRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _paymentRepository = paymentRepository;
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
    /*public async Task<IEnumerable<Event>> ListByEventIdAsync(int organizerId)
    {
        return await _eventRepository.ListByEventIdAsync(organizerId);
    }*/
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

    public async Task<EventResponse> AddPaymentToEvent(int eventId, int paymentId)
    {
        var events = await _eventRepository.FindByIdAsync(eventId);
        var payment = await _paymentRepository.FindByIdAsync(paymentId);

        if (events == null || payment == null)
            return new EventResponse("The event or the payment doesn't exist");

        try
        {
            events.PaymentsListByEvent.Add(payment);
            await _unitOfWork.CompleteAsync();
            return new EventResponse(events);
        }
        catch (Exception e)
        {
            return new EventResponse($"An error occurred while adding the payment to event: {e.Message}");
        }
    }
    public async Task<ActionResult<Event>> GetAsync(int id)
    {
        var eventItem = await _eventRepository.FindByIdAsync(id);
        if (eventItem == null)
        {
            return new NotFoundResult();
        }
        return new ActionResult<Event>(eventItem);

    }
}