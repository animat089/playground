using AnimatLabs.SourceGenerators.Attributes;
using AnimatLabs.SourceGenerators.Demo.Models;

namespace AnimatLabs.SourceGenerators.Demo.Mappers;

[GenerateMapper]
public partial class UserMapper
{
    public partial UserDto ToDto(User entity);
    public partial User ToEntity(UserDto dto);
}
