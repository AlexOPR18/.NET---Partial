namespace RoomHub.Api.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Booking>> GetActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken);

    Task<IReadOnlyList<Booking>> GetActiveForParticipantOnDayAsync(
        string bookedBy,
        DateTime dayUtc,
        CancellationToken cancellationToken);

    void Add(Booking booking);
}
