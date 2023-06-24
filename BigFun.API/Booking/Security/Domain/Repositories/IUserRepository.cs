using BigFun.API.Booking.Security.Domain.Models;

namespace BigFun.API.Booking.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();

    Task AddAsync(User user);

    Task<User> FindByIdAsync(int id);

    Task<User> FindByUsernameAsync(string username);

    public bool ExistByUsername(string username);

    User FindById(int id);

    void Update(User user);

    void Remove(User user);
}