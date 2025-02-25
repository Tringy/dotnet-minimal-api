using Domain.Users;

namespace Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
    public interface ITokenProvider
    {
        string Create(User user);
    }
}