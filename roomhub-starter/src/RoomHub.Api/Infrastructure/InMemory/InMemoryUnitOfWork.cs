using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Infrastructure.InMemory;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
}
