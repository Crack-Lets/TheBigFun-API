using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PaymentController
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;

    public PaymentController(IPaymentService paymentService, IMapper mapper, IEventRepository eventRepository)
    {
        _paymentService = paymentService;
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PaymentResource>> GetAllAsync()
    {
        var payments = await _paymentService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentResource>>(payments);
        return resources;
    }

    [HttpGet("{eventId}/payments")]
    public async Task<IEnumerable<PaymentResource>> GetPaymentsByEventIdAsync(int eventId)
    {
        var events = await _eventRepository.FindByIdAsync(eventId);
        if (events == null)
        {
            var errorMessage="the event doesn't exist";
            throw new ArgumentException("the event doesn't exist");
        }
        var paymentsByEvent = await _paymentService.ListByEventIdAsync(eventId);
        var resources = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentResource>>(paymentsByEvent);
        return resources;
    }
    
    /*public async Task<IActionResult>>AddPaymentToEvent(int ecentId, int paymentId)
    {
        
    }*/



}