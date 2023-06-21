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
public class AttendeesController : ControllerBase
{
    private readonly IAttendeeService _attendeeService;
    private readonly IMapper _mapper;

    public AttendeesController(IAttendeeService attendeeService, IMapper mapper)
    {
        _attendeeService = attendeeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<AttendeeResource>> GetAllAsync()
    {
        var attendees = await _attendeeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Attendee>, IEnumerable<AttendeeResource>>(attendees);

        return resources;
    }
    
    [HttpGet("{attendeeId}/events")]
    public async Task<IEnumerable<EventResource>> GetAllEventsByAttendeeAsync(int attendeeId)
    {
        var eventsByAttendee = await _attendeeService.ListEventsByAttendeeAsync(attendeeId);
        var resources = _mapper.Map<IEnumerable<Event>, IEnumerable<EventResource>>(eventsByAttendee);
        return resources;
    }
    
    
    

    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveAttendeeResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

        var attendee = _mapper.Map<SaveAttendeeResource, Attendee>(resource);

        var result = await _attendeeService.SaveAsync(attendee);

        if (!result.Success) return BadRequest(result.Message);

        var attendeeResource = _mapper.Map<Attendee, AttendeeResource>(result.Resource);

        return Ok(attendeeResource);
    }
    
    [HttpPost("{attendeeId}/events/{eventId}")]
    public async Task<IActionResult> AddEventToAttendee(int attendeeId,int eventId)
    {
        var result= await _attendeeService.AddEventToAttendee(attendeeId, eventId);

        if (!result.Success) return BadRequest(result.Message);
        return Ok("The event was added");

    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAttendeeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var attendee = _mapper.Map<SaveAttendeeResource, Attendee>(resource);

        var result = await _attendeeService.UpdateAsync(id, attendee);

        if (!result.Success) return BadRequest(result.Message);

        var attendeeResource = _mapper.Map<Attendee, AttendeeResource>(result.Resource);

        return Ok(attendeeResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _attendeeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var attendeeResource = _mapper.Map<Attendee, AttendeeResource>(result.Resource);

        return Ok(attendeeResource);
    }
    

}