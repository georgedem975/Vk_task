namespace Core.Services;

public interface IUserAuthenticationService
{
    Task UserAuthenticated(string login, string password);
}