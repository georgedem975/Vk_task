namespace DataAccess.Exceptions;

public class UserException : Exception
{
    private UserException(string message)
        : base(message)
    { }

    public static UserException NotFoundByIdException(string repositoryName, string methodName, long id) =>
        new UserException($"user with id: {id} not found in repository: {repositoryName} int method: {methodName}");
}