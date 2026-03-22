using FastEndpoints;

namespace AnimatLabs.FastEndpoints.Endpoints;

public class GetUsersEndpoint : EndpointWithoutRequest<List<CreateUserResponse>>
{
    public override void Configure()
    {
        Get("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = new List<CreateUserResponse>
        {
            new() { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new() { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };
        await SendAsync(users);
    }
}
