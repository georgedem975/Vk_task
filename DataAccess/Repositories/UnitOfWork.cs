using DataAccess.DatabaseContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Context _context;

    private IRepository<User> _userRepository;

    public UnitOfWork(DbContextOptions<Context> options) => _context = new Context(options);

    public IRepository<User> UserRepository
    {
        get
        {
            if (_userRepository is null)
            {
                _userRepository = new UserRepository(_context);
            }

            return _userRepository;
        }
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}