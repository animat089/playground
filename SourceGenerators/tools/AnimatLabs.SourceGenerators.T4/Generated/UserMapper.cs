namespace AnimatLabs.SourceGenerators.T4.Generated;

public partial class UserMapper
{
    public UserDto ToDto(User entity)
    {
        if (entity == null)
        {
            return null!;
        }

        var target = new UserDto();
        target.Id = entity.Id;
        target.FirstName = entity.FirstName;
        target.LastName = entity.LastName;
        target.Email = entity.Email;
        return target;
    }
}
