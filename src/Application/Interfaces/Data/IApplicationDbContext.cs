using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
