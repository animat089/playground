using HashidsNet;
using Sample.Hashed.Services.Contracts;

namespace Sample.Hashed.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IHashids hashids;
        private readonly IEnumerable<Models.Entity.User> users;

        public UserService(IHashids hashids)
        {
            this.hashids = hashids;
            users = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Models.Entity.User>>(File.ReadAllText("UserDetails.json"));
        }

        public Models.View.User GetUserById(string userId)
        {
            var rawId = hashids.DecodeLong(userId);

            if (rawId.Length == 0)
                return null;

            return MapEntityToView(users.FirstOrDefault(x => x.Id == rawId[0]));
        }

        public IEnumerable<Models.View.User> GetAllUsers()
        {
            return users.Select(x => MapEntityToView(x));
        }

        private Models.View.User MapEntityToView(Models.Entity.User? user)
        {
            if (user == null)
                return null;

            return new Models.View.User
            {
                Id = hashids.EncodeLong(user.Id),
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}