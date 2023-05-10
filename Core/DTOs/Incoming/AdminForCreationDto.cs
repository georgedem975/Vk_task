namespace Core.DTOs.Incoming;

public class AdminForCreationDto
{
    public string Login { get; set; }
    
    public string Password { get; set; }

    public string UserGroupDescription { get; set; }

    public string UserStateDescription { get; set; }
}