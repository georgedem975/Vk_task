namespace Core.Exceptions;

public class AccessException : Exception
{
    private AccessException(string message)
        : base(message)
    { }

    public static AccessException AccessDenied(string login) =>
        new AccessException($"user: {login} access denied");
}