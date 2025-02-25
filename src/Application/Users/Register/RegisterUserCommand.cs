using Application.Interfaces.Messaging;

namespace Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string UserName, string Password) : ICommand<Guid>;
