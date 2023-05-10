namespace DataAccess.Models;

public class UserGroup
{
    public UserGroup(string code, string description)
    {
        Code = code ?? throw new ArgumentNullException();
        Description = description ?? throw new ArgumentNullException();
    }

    public long Id { get; set; }

    public string Code { get; set; }

    public string Description { get; set; }

    public User? User { get; set; }
}