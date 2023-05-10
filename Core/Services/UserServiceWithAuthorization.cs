using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
using Core.Exceptions;
using DataAccess.Repositories;

namespace Core.Services;

public class UserServiceWithAuthorization : IUserService
{
    private IUserService _userService;

    public UserServiceWithAuthorization(IUnitOfWork unitOfWork, IMapper mapper) =>
        _userService = new UserService(unitOfWork, mapper);

    public async Task<UserDto> CreateUser(UserForCreationDto userForCreation) =>
        await _userService.CreateUser(userForCreation);

    public async Task<UserDto> CreateAdmin(AdminForCreationDto adminForCreation) =>
        await _userService.CreateAdmin(adminForCreation);

    public async Task<DeletedUserDto> DeleteUser(UserForDeleteDto userForDelete) =>
        await _userService.DeleteUser(userForDelete);

    public async Task<IReadOnlyCollection<UserDto>> GetAllActiveUsers(string login)
    {
        if ((await _userService.GetUserByUniqueLogin(login)).UserGroupCode != "Admin")
        {
            throw AccessException.AccessDenied(login);
        }
        
        return await _userService.GetAllActiveUsers(login);
    }

    public async Task<IReadOnlyCollection<UserDto>> GetActiveUsers(string login, int page, int count)
    {
        if ((await _userService.GetUserByUniqueLogin(login)).UserGroupCode != "Admin")
        {
            throw AccessException.AccessDenied(login);
        }
        
        return await _userService.GetActiveUsers(login, page, count);
    }

    public async Task<UserDto> GetUserByUniqueLogin(string login) =>
        await _userService.GetUserByUniqueLogin(login);
}