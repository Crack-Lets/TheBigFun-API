using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Controllers;

[ApiController]
[Route("/api/v1/organizers/{organizerId}/events")]
public class OrganizerEventsController: ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public OrganizerEventsController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }
    
    
    /*
    [HttpGet]
    public async Task<IEnumerable<EventResource>> GetAllByOrganizerIdAsync(int organizerId)
    {
        var events = await _eventService.ListByOrganizerIdAsync(organizerId);

        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(events);

        return resources;
    }*/
}