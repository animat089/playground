using FastEndpoints;
using FluentValidation;

namespace AnimatLabs.FastEndpoints.Endpoints;

public class CreateUserRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class CreateUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}

public class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}

public class CreateUserEndpoint : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var id = Random.Shared.Next(1000, 9999);
        await SendAsync(new CreateUserResponse
        {
            Id = id,
            Name = req.Name,
            Email = req.Email
        });
    }
}
