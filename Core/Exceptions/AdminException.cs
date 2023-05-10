namespace Core.Exceptions;

public class AdminException : Exception
{
    private AdminException(string message)
        : base(message) 
    { }

    public static AdminException AdminCountExceeded(string login)
    {
        string message = $"an admin with a login: {login} cannot be created because the number of admins has been exceeded";
        return new AdminException(message);
    }
}