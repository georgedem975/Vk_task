using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Web.BasicAuthentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserAuthenticationService _userService;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserAuthenticationService userService)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Authorization header not found.");
        }

        string authHeader = Request.Headers["Authorization"].ToString();

        if (!authHeader.StartsWith("Basic"))
        {
            return AuthenticateResult.Fail("Invalid authorization scheme.");
        }

        string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
        byte[] decodedBytes = Convert.FromBase64String(encodedUsernamePassword);
        string decodedUsernamePassword = Encoding.UTF8.GetString(decodedBytes);
        string[] parts = decodedUsernamePassword.Split(':', 2);

        string login = parts[0];
        string password = parts[1];

        try
        {
            await _userService.UserAuthenticated(login, password);
        }
        catch(Exception exception)
        {
            return AuthenticateResult.Fail(exception.Message);
        }
        
        var claims = new[] {
            new Claim(ClaimTypes.Name, login),
        };
        
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}