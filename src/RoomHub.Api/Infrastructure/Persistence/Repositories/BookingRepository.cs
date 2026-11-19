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

    public async Task<IReadOnlyList<Booking>> GetActiveForParticipantOnDayAsync(
        string bookedBy,
        DateTime dayUtc,
        CancellationToken cancellationToken)
    {
        var dayStart = DateTime.SpecifyKind(dayUtc.Date, DateTimeKind.Utc);
        var nextDayStart = dayStart.AddDays(1);

        return await db.Bookings
            .Where(booking => booking.BookedBy == bookedBy
                && booking.Status == BookingStatus.Active
                && booking.Period.StartUtc >= dayStart
                && booking.Period.StartUtc < nextDayStart)
            .ToListAsync(cancellationToken);
    }

    public void Add(Booking booking) => db.Bookings.Add(booking);
}
