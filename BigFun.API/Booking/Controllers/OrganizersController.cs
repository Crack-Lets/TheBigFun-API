using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using BigFun.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class OrganizersController:ControllerBase
{
    private readonly IOrganizerService _organizerService;
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly IOrganizerRepository _organizerRepository;


    public OrganizersController(IOrganizerService organizerService, IMapper mapper, IOrganizerRepository organizerRepository, IEventService eventService)
    {
        _organizerService = organizerService;
        _mapper = mapper;
        _organizerRepository = organizerRepository;
        _eventService = eventService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<OrganizerResource>> GetAllAsync()
    {
        var organizers = await _organizerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Organizer>, IEnumerable<OrganizerResource>>(organizers);

        return resources;
    }
    
    [HttpGet("{organizerId}/events")]
    public async Task<IEnumerable<EventResource>> GetAllEventsByOrganizerAsync(int organizerId)
    {
        var eventsByOrganizer = await _organizerService.ListEventsByOrganizerAsync(organizerId);
        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(eventsByOrganizer);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrganizerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var category = _mapper.Map<SaveOrganizerResource, Organizer>(resource);

        var result = await _organizerService.SaveAsync(category);

        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(categoryResource);
    }
    
    [HttpPost("{organizerId}/events")]
    public async Task<IActionResult> OrganizerCreateEvent(int organizerId,[FromBody] SaveEventResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

        var organizer = await _organizerRepository.FindByIdAsync(organizerId);
        
        if (organizer == null) return NotFound("Organizer doesn't exist");

        var events = _mapper.Map<SaveEventResource, Event>(resource);

        events.OrganizerId = organizerId;

        var result = await _eventService.SaveAsync(events);

        if (!result.Success) return BadRequest(result.Message);

        var eventResource = _mapper.Map<Event, EventResource>(result.Resource);

        await _organizerService.AddEventToOrganizer(organizerId, result.Resource.Id);

        return Ok(eventResource);
            
    }

    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrganizerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var category = _mapper.Map<SaveOrganizerResource, Organizer>(resource);
        var result = await _organizerService.UpdateAsync(id, category);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var categoryResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(categoryResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _organizerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var categoryResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(categoryResource);
    }

}