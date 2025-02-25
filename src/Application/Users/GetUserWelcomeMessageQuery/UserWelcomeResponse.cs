namespace Application.Users.GetById;

public sealed record UserWelcomeResponse
{
    public required string Message { get; init; }
}
