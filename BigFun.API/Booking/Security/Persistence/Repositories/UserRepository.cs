using BigFun.API.Booking.Security.Domain.Models;
using BigFun.API.Booking.Security.Domain.Repositories;
using BigFun.API.Shared.Persistence.Contexts;
using BigFun.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BigFun.API.Booking.Security.Persistence.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {

       await _context.Users.AddAsync(user);
    }

    public Task<User> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        return await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public bool ExistByUsername(string username)
    {
        return _context.Users.Any(x => x.Username == username);
    }

    public User FindById(int id)
    {
       return _context.Users.Find(id);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
}