using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Controllers;


[ApiController]
[Route("/api/v1[controller]")]
public class EventsController: ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;


    public EventsController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<EventResource>> GetAllAsync()
    {
        var events = await _eventService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(events);
        return resources;
    }
}