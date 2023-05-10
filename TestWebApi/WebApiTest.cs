using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;
using Xunit;
namespace TestWebApi;

public class WebApiTest
{
    private readonly UserController _controller;
    private readonly Mock<IUserService> _userServiceMock;

    public WebApiTest()
    {
        _userServiceMock = new Mock<IUserService>();

        _controller = new UserController(_userServiceMock.Object);
    }
    
    [Fact]
    public async Task CreateUser_ReturnsOkResult()
    {
        // Arrange
        var userForCreationDto = new UserForCreationDto()
        {
            Login = "John",
            Password = "rwerkwe",
            UserGroupDescription = "-",
            UserStateDescription = "-"
        };

        var userDto = new UserDto()
        {
            Login = "John",
            UserGroupCode = "User",
            UserGroupDescription = "-",
            UserStateCode = "Active", 
            UserStateDescription = "-"
        };

        _userServiceMock.Setup(service => service.CreateUser(userForCreationDto)).ReturnsAsync(userDto);

        // Act
        var result = await _controller.CreateUser(userForCreationDto);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateAdmin_ReturnsOkResult()
    {
        // Arrange
        var adminForCreationDto = new AdminForCreationDto()
        {
            Login = "John",
            Password = "rwerkwe",
            UserGroupDescription = "-",
            UserStateDescription = "-"
        };

        var userDto = new UserDto()
        {
            Login = "John",
            UserGroupCode = "User",
            UserGroupDescription = "-",
            UserStateCode = "Active", 
            UserStateDescription = "-"
        };

        _userServiceMock.Setup(service => service.CreateAdmin(adminForCreationDto)).ReturnsAsync(userDto);

        // Act
        var result = await _controller.CreateAdmin(adminForCreationDto);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }
}