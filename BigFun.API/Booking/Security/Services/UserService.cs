using AutoMapper;
using BigFun.API.Booking.Security.Authorization.Handlers.Interfaces;
using BigFun.API.Booking.Security.Domain.Models;
using BigFun.API.Booking.Security.Domain.Repositories;
using BigFun.API.Booking.Security.Domain.Services;
using BigFun.API.Booking.Security.Domain.Services.Communication;
using BigFun.API.Booking.Security.Exceptions;
using BigFun.API.Shared.Domain.Repositories;
using BcryptNet = BCrypt.Net.BCrypt;
namespace BigFun.API.Booking.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }


    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName},{user.Username},{user.PasswordHash}");


        if (user == null || !BcryptNet.Verify(request.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }

        Console.WriteLine("Authentication successful. About to generate token");
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName}, {response.LastName}, {response.Username}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if (_userRepository.ExistByUsername(request.Username))
            throw new AppException("Username '" + request.Username + "' is already taken");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = BcryptNet.HashPassword(request.Password);


        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);

        if (_userRepository.ExistByUsername(request.Username))
            throw new AppException("Username '" + request.Username + "' is already taken");

        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BcryptNet.HashPassword(request.Password);

        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();

        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }



}