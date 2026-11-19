using System.Collections.Concurrent;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Infrastructure.InMemory;

public sealed class InMemoryRoomRepository : IRoomRepository
{
    private readonly ConcurrentDictionary<Guid, Room> _rooms = new();

    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Task.FromResult(_rooms.GetValueOrDefault(id));

    public void Add(Room room) => _rooms[room.Id] = room;
}
