namespace Core.Exceptions;

public class UserCreationException : Exception
{
    private UserCreationException(string message)
        : base(message)
    { }

    public static UserCreationException UserAlreadyCreated(string login) =>
        new UserCreationException($"user with login: {login} already created");

    public static UserCreationException UserAlreadyRegistering(string login) =>
        new UserCreationException($"user with login: {login} already registering");
}