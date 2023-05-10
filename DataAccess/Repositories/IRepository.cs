using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyCollection<User>> GetAll();

    Task<T?> FindByUniqueLogin(string login);

    Task<int> CountAdmins();

    Task Create(T item);

    Task Delete(long id);
}