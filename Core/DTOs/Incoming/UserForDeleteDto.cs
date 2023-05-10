namespace Core.DTOs.Incoming;

public class UserForDeleteDto
{
    public string Login { get; set; }
    
    public string Password { get; set; }
    
    public string UserGroupCode { get; set; }
}