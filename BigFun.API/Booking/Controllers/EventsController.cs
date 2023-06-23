using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using BigFun.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BigFun.API.Booking.Controllers;


[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, read, update and delete Attendees")]
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
    
    [HttpGet("events/{id}")]
    public async Task<IEnumerable<EventResource>> GetEventById(int id)
    {
        var events = await _eventService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(events);
        return resources;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveEventResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

        var events = _mapper.Map<SaveEventResource, Event>(resource);

        var result = await _eventService.SaveAsync(events);

        if (!result.Success) return BadRequest(result.Message);

        var eventResource = _mapper.Map<Event, EventResource>(result.Resource);

        return Ok(eventResource);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<EventResource>> GetByEventId(int id)
    {
        var eventItem = await _eventService.GetAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        var resource = _mapper.Map<Event, EventResource>(eventItem.Value);
        return resource;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEventResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var events = _mapper.Map<SaveEventResource, Event>(resource);

        var result = await _eventService.UpdateAsync(id, events);

        if (!result.Success) return BadRequest(result.Message);

        var attendeeResource = _mapper.Map<Event, EventResource>(result.Resource);

        return Ok(attendeeResource);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _eventService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var attendeeResource = _mapper.Map<Event, EventResource>(result.Resource);

        return Ok(attendeeResource);
    }
    
    
    
    
    
    
    
    
}