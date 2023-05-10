using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException();
    }

    [HttpPost("createUser")]
    public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto userForCreation)
    {
        UserDto user = await _userService.CreateUser(userForCreation);

        return new OkObjectResult(user);
    }

    [HttpPost("createAdmin")]
    public async Task<ActionResult<UserDto>> CreateAdmin(AdminForCreationDto adminForCreation)
    {
        UserDto admin = await _userService.CreateAdmin(adminForCreation);
        
        return new OkObjectResult(admin);
    }

    [HttpGet("getAllActiveUsers/{login}")]
    [Authorize(AuthenticationSchemes = "Basic")]
    public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetAllActiveUsers(string login)
    {
        List<UserDto> users;
        try
        {
            users = (await _userService.GetAllActiveUsers(login)).ToList();
        }
        catch (Exception exception)
        {
            return new BadRequestResult();
        }

        return new OkObjectResult(users);
    }

    [HttpGet("getActiveUsers/{login}")]
    [Authorize(AuthenticationSchemes = "Basic")]
    public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetActiveUsers(string login, [FromQuery] int page = 1, [FromQuery] int count = 5)
    {
        List<UserDto> users;
        try
        {
            users = (await _userService.GetActiveUsers(login, page, count)).ToList();
        }
        catch (Exception exception)
        {
            return new BadRequestResult();
        }
        
        return new OkObjectResult(users);
    }

    [HttpGet("getUser/{login}")]
    [Authorize(AuthenticationSchemes = "Basic")]
    public async Task<ActionResult<UserDto>> GetUserByLogin(string login)
    {
        UserDto user;
        try
        {
            user = await _userService.GetUserByUniqueLogin(login);
        }
        catch (Exception exception)
        {
            return new BadRequestResult();
        }

        return new OkObjectResult(user);
    }

    [HttpDelete("DeleteUser")]
    [Authorize(AuthenticationSchemes = "Basic")]
    public async Task<ActionResult<UserDto>> DeleteUser(UserForDeleteDto userForDelete)
    {
        DeletedUserDto user;
        try
        {
             user = await _userService.DeleteUser(userForDelete);
        }
        catch (Exception exception)
        {
            return new BadRequestResult();
        }
        
        return new OkObjectResult(user);
    }
}