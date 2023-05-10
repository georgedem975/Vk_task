namespace DataAccess.Models;

public class User
{
    public long Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public DateTime CreatedDate { get; set; }

    public long UserGroupId { get; set; }

    public UserGroup UserGroup { get; set; }

    public long UserStateId { get; set; }

    public UserState UserState { get; set; }
}