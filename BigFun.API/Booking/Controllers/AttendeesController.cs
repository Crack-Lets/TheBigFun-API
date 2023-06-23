using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using BigFun.API.Booking.Services;
using BigFun.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BigFun.API.Booking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Create, read, update and delete Attendee")]
public class AttendeesController : ControllerBase
{
    private readonly IAttendeeService _attendeeService;
    private readonly IMapper _mapper;
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IPaymentService _paymentService;
    private readonly IEventService _eventService;

    public AttendeesController(IAttendeeService attendeeService, IMapper mapper, IAttendeeRepository attendeeRepository, IEventRepository eventRepository, IPaymentService paymentService, IEventService eventService)
    {
        _attendeeService = attendeeService;
        _mapper = mapper;
        _attendeeRepository = attendeeRepository;
        _eventRepository = eventRepository;
        _paymentService = paymentService;
        _eventService = eventService;
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
    
    
    
    [HttpPost("{attendeeId}/events/{eventId}/payment")]
    public async Task<IActionResult> AttendeeCreatePayment(int attendeeId,int eventId,[FromBody] SavePaymentResource resource)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

        var attendee =await _attendeeRepository.FindByIdAsync(attendeeId);
        var events =await _eventRepository.FindByIdAsync(eventId);

        if (attendee == null) return NotFound("The Attendee doesn't exist");
        if (events == null) return NotFound("The Event doesn't exist");

        var payment = _mapper.Map<SavePaymentResource, Payment>(resource);

        payment.EventId = eventId;
        payment.AttendeeId = attendeeId;

        var result = await _paymentService.SaveAsync(payment);

        if (!result.Success) return BadRequest(result.Message);

        var paymentResource = _mapper.Map<Payment, PaymentResource>(result.Resource);

        await _eventService.AddPaymentToEvent(eventId, result.Resource.Id);

        await _attendeeService.AddPaymentToAttendee(attendeeId, result.Resource.Id);
        
        return Ok(paymentResource);
        
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