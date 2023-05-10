namespace Core.Exceptions;

public class UserAuthenticatedException : Exception
{
    private UserAuthenticatedException(string message) 
        : base(message) 
    { }

    public static UserAuthenticatedException UserWithLoginNotFound(string login) =>
        new UserAuthenticatedException($"user with login: {login} not found");

    public static UserAuthenticatedException WrongUserPassword(string login, string password) =>
        new UserAuthenticatedException(
            $"the user with login: {login} has a different password, password: {password} is incorrect");
}