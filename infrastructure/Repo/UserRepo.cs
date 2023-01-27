using Domain.AccountProperties.UserEntity;
using Infrastructure.db;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo;

public class UserRepo
{
    private readonly StockMarketContext _context;
    public UserRepo(StockMarketContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserById(Guid id)
        => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task UpdateUser(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}