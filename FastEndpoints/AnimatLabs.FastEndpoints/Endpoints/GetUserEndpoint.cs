using FastEndpoints;

namespace AnimatLabs.FastEndpoints.Endpoints;

public class GetUserRequest
{
    public int Id { get; set; }
}

public class GetUserEndpoint : Endpoint<GetUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Get("/api/users/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        await SendAsync(new CreateUserResponse
        {
            Id = req.Id,
            Name = $"User {req.Id}",
            Email = $"user{req.Id}@example.com"
        });
    }
}
