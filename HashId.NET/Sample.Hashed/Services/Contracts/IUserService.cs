using Sample.Hashed.Models.View;

namespace Sample.Hashed.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string userId);

        IEnumerable<User> GetAllUsers();
    }
}