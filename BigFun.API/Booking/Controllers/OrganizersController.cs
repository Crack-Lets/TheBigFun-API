using AutoMapper;
using BigFun.API.Booking.Domain.Models;
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
    private readonly IMapper _mapper;


    public OrganizersController(IOrganizerService organizerService, IMapper mapper)
    {
        _organizerService = organizerService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<OrganizerResource>> GetAllAsync()
    {
        var organizers = await _organizerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Organizer>, IEnumerable<OrganizerResource>>(organizers);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrganizerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var organizer = _mapper.Map<SaveOrganizerResource, Organizer>(resource);

        var result = await _organizerService.SaveAsync(organizer);

        if (!result.Success)
            return BadRequest(result.Message);

        var organizerResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(organizerResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrganizerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var organizer = _mapper.Map<SaveOrganizerResource, Organizer>(resource);

        var result = await _organizerService.UpdateAsync(id, organizer);

        if (!result.Success)
            return BadRequest(result.Message);

        var organizerResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(organizerResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _organizerService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var organizerResource = _mapper.Map<Organizer, OrganizerResource>(result.Resource);

        return Ok(organizerResource);
    }

}