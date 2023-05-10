namespace Core.DTOs.Outgoing;

public class UserDto
{
    public string Login { get; set; }
    
    public DateTime CreatedDate { get; set; }
    
    public string UserGroupCode { get; set; }

    public string UserGroupDescription { get; set; }
    
    public string UserStateCode { get; set; }

    public string UserStateDescription { get; set; }
}