using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;

namespace Core.Services;

public interface IUserService
{
    Task<UserDto> CreateUser(UserForCreationDto userForCreation);

    Task<UserDto> CreateAdmin(AdminForCreationDto adminForCreation);

    Task<DeletedUserDto> DeleteUser(UserForDeleteDto userForDelete);

    Task<IReadOnlyCollection<UserDto>> GetAllActiveUsers(string login);

    Task<IReadOnlyCollection<UserDto>> GetActiveUsers(string login, int page, int count);

    Task<UserDto> GetUserByUniqueLogin(string login);
}