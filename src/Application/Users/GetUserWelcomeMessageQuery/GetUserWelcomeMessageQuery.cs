using Application.Interfaces.Messaging;

namespace Application.Users.GetById;

public sealed record GetUserWelcomeMessageQuery(string UserEmail) : IQuery<UserWelcomeResponse>;
