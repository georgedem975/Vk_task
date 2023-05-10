using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }

    Task SaveAsync();
}