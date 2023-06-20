using AutoMapper;
using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BigFun.API.Booking.Controllers;

[ApiController]
[Route("/api/v1/organizers/{organizerId}/payments")]
public class OrganizerPaymentsController: ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;

    public OrganizerPaymentsController(IPaymentService paymentService, IMapper mapper)
    {
        _paymentService = paymentService;
        _mapper = mapper;
    }
    
    
    /*
    [HttpGet]
    public async Task<IEnumerable<PaymentResource>> GetAllByOrganizerIdAsync(int organizerId)
    {
        var payments = await _paymentService.ListByOrganizerIdAsync(organizerId);

        var resources = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentResource>>(payments);

        return resources;
    }*/
    
    // booking-> services->PaymentService
    /*
     public async Task<IEnumerable<Payment>> ListByOrganizerIdAsync(int organizerId)
     {
        return await _paymentRepository.FindByOrganizerIdAsync(organizerId);
      }
     */
    
  // booking-> persistence->repositories->PaymentRepository
     /*
       public async Task<IEnumerable<Payment>> FindByOrganizerIdAsync(int organizerId)
     {
         return await _context.Payments
             .Where(p => p.OrganizerId == organizerId)
               .Include(p => p.Organizer)
            .ToListAsync();
    }
     */
    
     // booking-> domain->services-> IPaymentService
     /*
        Task<IEnumerable<Payment>> ListByOrganizerIdAsync(int organizerId);

     */
     
}