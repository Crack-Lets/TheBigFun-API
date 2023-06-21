using BigFun.API.Booking.Domain.Models;
using BigFun.API.Booking.Domain.Repositories;
using BigFun.API.Booking.Domain.Services;
using BigFun.API.Booking.Domain.Services.Communication;
using BigFun.API.Shared.Domain.Repositories;

namespace BigFun.API.Booking.Services;

public class AttendeeService: IAttendeeService
{

    private readonly IAttendeeRepository _attendeeRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AttendeeService(IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork)
    {
        _attendeeRepository = attendeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Attendee>> ListAsync()
    {
        return await _attendeeRepository.ListAsync();
    }

    public async Task<AttendeeResponse> SaveAsync(Attendee attendee)
    {
        try
        {
            await _attendeeRepository.AddAsync(attendee);
            await _unitOfWork.CompleteAsync();

            return new AttendeeResponse(attendee);
        }
        catch (Exception e)
        {
            return new AttendeeResponse($"An error ocurred while saving the attendee : {e.Message}");
        }
    }

    public async Task<AttendeeResponse> UpdateAsync(int id, Attendee attendee)
    {
        
        var existingAttendee = await _attendeeRepository.FindByIdAsync(id);
        if (existingAttendee==null)
        {
            return new AttendeeResponse("Attendee not found.");
        }
        
        existingAttendee.Name = attendee.Name;
        existingAttendee.UserName = attendee.UserName;
        existingAttendee.Email = attendee.Email;
        //existingAttendee.Events = attendee.Events;
        //existingAttendee.Payments = attendee.Payments;
        
        try
        {
            _attendeeRepository.Update(existingAttendee);
            await _unitOfWork.CompleteAsync();

            return new AttendeeResponse(existingAttendee);
        }
        catch (Exception e)
        {
            return new AttendeeResponse($"An error ocurred while updatting the attendee : {e.Message}");
        }
    }

    public async Task<AttendeeResponse> DeleteAsync(int id)
    {
        var existingAttendee = await _attendeeRepository.FindByIdAsync(id);
        if (existingAttendee==null)
        {
            return new AttendeeResponse("Attendee not found.");
        }

        try
        {
            _attendeeRepository.Remove(existingAttendee);
            await _unitOfWork.CompleteAsync();

            return new AttendeeResponse(existingAttendee);
        }
        catch (Exception e)
        {
            return new AttendeeResponse($"An error ocurred while deleting the attendee : {e.Message}");
        }
    }
}