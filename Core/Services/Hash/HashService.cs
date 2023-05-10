using System.Security.Cryptography;
using System.Text;

namespace Core.Services.Hash;

public class HashService : IHashService
{
    public string GetHashPassword(string password)
    {
        string passwordWithSalt = GetPasswordWithSalt(password);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt));
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }

    private string GetPasswordWithSalt(string password)
    {
        int firstIndexPosition = 0;
        int middleIndexPosition = password.Length / 2;
        int lengthOfSalt = 2;
        
        string salt = password.Substring(middleIndexPosition, lengthOfSalt);

        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(salt)
            .Append(password.Substring(firstIndexPosition, middleIndexPosition))
            .Append(salt)
            .Append(password.Substring(middleIndexPosition))
            .Append(salt);

        return stringBuilder.ToString();
    }
}