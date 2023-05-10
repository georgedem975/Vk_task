using Core.Exceptions;
using Core.Services.Hash;
using DataAccess.Repositories;

namespace Core.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private IHashService _hashService;

    private IUnitOfWork _unitOfWork;

    public UserAuthenticationService(IHashService hashService, IUnitOfWork unitOfWork)
    {
        _hashService = hashService ?? throw new ArgumentNullException();
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
    }
    
    public async Task UserAuthenticated(string login, string password)
    {
        string passwordHash = _hashService.GetHashPassword(password);

        var user = await _unitOfWork.UserRepository.FindByUniqueLogin(login);

        if (user is null)
        {
            throw UserAuthenticatedException.UserWithLoginNotFound(login);
        }

        if (passwordHash != user.Password)
        {
            throw UserAuthenticatedException.WrongUserPassword(login, password);
        }
    }
}