using PostSharp.Aspects;
using PostSharp.Serialization;

namespace AspectOrientedProgrammingPostSharp.Aspects;

[PSerializable]
public class SecurityAspect : OnMethodBoundaryAspect
{
    [PNonSerialized]
    private readonly string _requiredRole;

    public SecurityAspect(string requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var user = SecurityContext.CurrentUser;

        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        if (user.Role != _requiredRole)
        {
            throw new UnauthorizedAccessException($"User {user.Username} does not have permission to access this method.");
        }

        Console.WriteLine($"User {user.Username} is authorized to access {args.Method.Name}.");
    }
}
