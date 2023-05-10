using AutoMapper;
using Core.DTOs.Incoming;
using Core.DTOs.Outgoing;
using Core.Exceptions;
using Core.ExecutionQueueOfNames;
using DataAccess.Models;
using DataAccess.Repositories;

namespace Core.Services;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;

    private IMapper _mapper;

    private IExecutionQueue _executionQueue;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        _mapper = mapper ?? throw new ArgumentNullException();
        _executionQueue = new ExecutionQueue();
    }

    public async Task<UserDto> CreateAdmin(AdminForCreationDto adminForCreation)
    {
        await CheckUser(adminForCreation.Login);

        if (await _unitOfWork.UserRepository.CountAdmins() > 0)
        {
            throw AdminException.AdminCountExceeded(adminForCreation.Login);
        }
        
        _executionQueue.AddToQueue(adminForCreation.Login);
        
        var user = _mapper.Map<User>(adminForCreation);
        user.UserGroup.User = user;
        user.UserState.User = user;

        await CreateUser(user);
        
        _executionQueue.RemoveFromQueue(user.Login);
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUser(UserForCreationDto userForCreation)
    {
        await CheckUser(userForCreation.Login);
        
        _executionQueue.AddToQueue(userForCreation.Login);
        
        var user = _mapper.Map<User>(userForCreation);
        user.UserGroup.User = user;
        user.UserState.User = user;

        await CreateUser(user);
        
        _executionQueue.RemoveFromQueue(user.Login);
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<DeletedUserDto> DeleteUser(UserForDeleteDto userForDelete)
    {
        var user = await _unitOfWork.UserRepository.FindByUniqueLogin(userForDelete.Login);
        if (user is not null)
        {
            await _unitOfWork.UserRepository.Delete(user.Id);
            await _unitOfWork.SaveAsync();
        }

        return _mapper.Map<DeletedUserDto>(user);
    }

    public async Task<IReadOnlyCollection<UserDto>> GetAllActiveUsers(string login) =>
        (await _unitOfWork.UserRepository.GetAll())
            .Where(u => u.UserState.Code == "Active")
            .Select(u => _mapper.Map<UserDto>(u))
            .ToList()
            .AsReadOnly();

    public async Task<IReadOnlyCollection<UserDto>> GetActiveUsers(string login, int page, int count) =>
        (await GetAllActiveUsers(login)).Skip(page * count).Take(count).ToList().AsReadOnly();

    public async Task<UserDto> GetUserByUniqueLogin(string login) =>
        _mapper.Map<UserDto>(await _unitOfWork.UserRepository.FindByUniqueLogin(login));
    
    private async Task CreateUser(User user)
    {
        await _unitOfWork.UserRepository.Create(user);
        await _unitOfWork.SaveAsync();
    }

    private async Task CheckUser(string login)
    {
        if (await _unitOfWork.UserRepository.FindByUniqueLogin(login) is not null)
        {
            throw UserCreationException.UserAlreadyCreated(login);
        }

        if (_executionQueue.NameAlreadyExists(login))
        {
            throw UserCreationException.UserAlreadyRegistering(login);
        }
    }
}