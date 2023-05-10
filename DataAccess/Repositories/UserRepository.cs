using DataAccess.DatabaseContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UserRepository : IRepository<User>
{
    private Context _context;

    public UserRepository(Context context) =>
        _context = context ?? throw new ArgumentNullException();

    public async Task<IReadOnlyCollection<User>> GetAll() =>
        (await _context
            .Users
            .Include(u => u.UserGroup)
            .Include(u => u.UserState)
            .ToListAsync())
            .AsReadOnly();
    
    public async Task<User?> FindByUniqueLogin(string login) =>
        (await GetAll())
        .FirstOrDefault(u => u.Login == login);

    public async Task<int> CountAdmins() =>
        (await _context
            .Users
            .Include(u => u.UserGroup)
            .Where(u => u.UserGroup.Code == "Admin")
            .ToListAsync()).Count;

    public async Task Create(User user)
    {
        await _context.UserGroups.AddAsync(user.UserGroup);
        await _context.UserStates.AddAsync(user.UserState);

        await _context.Users.AddAsync(user);
    }

    public async Task Delete(long id)
    {
        User? user = await _context.Users.FindAsync(id);
        
        if (user is not null)
        {
            user.UserState.Code = "Blocked";
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}