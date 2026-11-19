using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public sealed class Room
{
    public const int MaxNameLength = 100;

    private Room()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Capacity { get; private set; }

    public static Result<Room> Create(string? name, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RoomErrors.NameRequired;

        if (name.Trim().Length > MaxNameLength)
            return RoomErrors.NameTooLong;

        if (capacity <= 0)
            return RoomErrors.CapacityMustBePositive;

        return new Room
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Capacity = capacity
        };
    }

    public Result<Booking> Book(
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc,
        IReadOnlyCollection<Booking> activeBookings)
    {
        var bookingResult = Booking.Create(Id, bookedBy, startUtc, endUtc, attendees, nowUtc);
        if (bookingResult.IsFailure)
            return bookingResult.Error;

        var booking = bookingResult.Value;

        if (booking.Attendees > Capacity)
            return BookingErrors.AttendeesExceedCapacity;

        if (activeBookings.Any(existing => existing.Blocks(booking.Period)))
            return BookingErrors.RoomOccupied;

        return booking;
    }
}
