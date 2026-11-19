using System.Collections.Concurrent;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.InMemory;

public sealed class InMemoryBookingRepository : IBookingRepository
{
    private readonly ConcurrentDictionary<Guid, Booking> _bookings = new();

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        Task.FromResult(_bookings.GetValueOrDefault(id));

    public Task<IReadOnlyList<Booking>> GetActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken)
    {
        IReadOnlyList<Booking> active = _bookings.Values
            .Where(booking => booking.RoomId == roomId && booking.IsActive)
            .ToList();

        return Task.FromResult(active);
    }

    public void Add(Booking booking) => _bookings[booking.Id] = booking;
}
