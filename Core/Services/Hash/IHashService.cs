namespace Core.Services.Hash;

public interface IHashService
{
    public string GetHashPassword(string password);
}