using Application.Interfaces.Data;
using Application.Interfaces.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.GetById;

internal sealed class GetUserByUserNameQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetUserWelcomeMessageQuery, UserWelcomeResponse>
{
    public async Task<Result<UserWelcomeResponse>> Handle(GetUserWelcomeMessageQuery query, CancellationToken cancellationToken)
    {
        UserWelcomeResponse? user = await context.Users
            .Where(u => u.Email == query.UserEmail)
            .Select(u => new UserWelcomeResponse
            {
                Message = $"Hello, {u.UserName}!",
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserWelcomeResponse>(UserErrors.NotFoundByEmail);
        }

        return user;
    }
}
