using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain.Bookings;

namespace RoomHub.Api.Infrastructure.Persistence.Repositories;

public sealed class BookingRepository(RoomHubDbContext db) : IBookingRepository
{
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        db.Bookings.FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Booking>> GetActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken) =>
        await db.Bookings
            .Where(booking => booking.RoomId == roomId && booking.Status == BookingStatus.Active)
            .ToListAsync(cancellationToken);

    public void Add(Booking booking) => db.Bookings.Add(booking);
}
