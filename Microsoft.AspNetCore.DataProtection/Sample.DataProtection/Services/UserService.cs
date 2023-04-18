using Microsoft.AspNetCore.DataProtection;
using Sample.Hashed.Services.Contracts;

namespace Sample.Hashed.Services;

public sealed class UserService : IUserService
{
    private readonly IDataProtector dataProtector;
    private readonly IEnumerable<Models.Entity.User> users;

    public UserService(IDataProtector hashids)
    {
        this.dataProtector = hashids;
        users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Models.Entity.User>>(File.ReadAllText("UserDetails.json"));
    }

    public Models.View.User GetUserById(string userId)
    {
        var rawId = Convert.ToInt64(dataProtector.Unprotect(userId));

        return MapEntityToView(users.FirstOrDefault(x => x.Id == rawId));
    }

    public IEnumerable<Models.View.User> GetAllUsers()
    {
        return users.Select(x => MapEntityToView(x));
    }

    private Models.View.User MapEntityToView(Models.Entity.User user)
    {
        if (user == null)
            return null;

        return new Models.View.User
        {
            Id = dataProtector.Protect(user.Id.ToString()),
            Name = user.Name,
            Email = user.Email
        };
    }
}