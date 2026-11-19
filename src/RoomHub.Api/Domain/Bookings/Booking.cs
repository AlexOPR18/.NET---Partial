using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public sealed class Booking
{
    public const int MaxBookedByLength = 100;

    public const int MaxActiveBookingsPerParticipantPerDay = 2;

    public static readonly TimeSpan MaxDuration = TimeSpan.FromHours(4);

    public static readonly TimeSpan MinNoticeForCancellation = TimeSpan.FromMinutes(60);

    private Booking()
    {
    }

    public Guid Id { get; private set; }

    public Guid RoomId { get; private set; }

    public string BookedBy { get; private set; } = string.Empty;

    public TimeRange Period { get; private set; } = null!;

    public int Attendees { get; private set; }

    public BookingStatus Status { get; private set; }

    public bool IsActive => Status == BookingStatus.Active;

    public DateTime DayUtc => Period.StartUtc.Date;

    internal static Result<Booking> Create(
        Guid roomId,
        string? bookedBy,
        DateTime startUtc,
        DateTime endUtc,
        int attendees,
        DateTime nowUtc)
    {
        if (string.IsNullOrWhiteSpace(bookedBy))
            return BookingErrors.BookedByRequired;

        if (bookedBy.Trim().Length > MaxBookedByLength)
            return BookingErrors.BookedByTooLong;

        var periodResult = TimeRange.Create(startUtc, endUtc);
        if (periodResult.IsFailure)
            return periodResult.Error;

        var period = periodResult.Value;

        if (attendees <= 0)
            return BookingErrors.AttendeesMustBePositive;

        if (period.StartUtc < nowUtc)
            return BookingErrors.StartInPast;

        if (period.Duration > MaxDuration)
            return BookingErrors.DurationTooLong;

        return new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            BookedBy = bookedBy.Trim(),
            Period = period,
            Attendees = attendees,
            Status = BookingStatus.Active
        };
    }

    public bool Blocks(TimeRange requested) => IsActive && Period.Overlaps(requested);

    public Result<Unit> EnsureWithinDailyLimit(IReadOnlyCollection<Booking> participantBookingsSameDay)
    {
        var otherActiveBookingsSameDay = participantBookingsSameDay.Count(other =>
            other.Id != Id
            && other.IsActive
            && other.BookedBy == BookedBy
            && other.DayUtc == DayUtc);

        if (otherActiveBookingsSameDay >= MaxActiveBookingsPerParticipantPerDay)
            return BookingErrors.DailyLimitReached;

        return Unit.Value;
    }

    public Result<Unit> Cancel(DateTime nowUtc)
    {
        if (!IsActive)
            return BookingErrors.AlreadyCancelled;

        if (Period.StartUtc - nowUtc < MinNoticeForCancellation)
            return BookingErrors.CancellationTooLate;

        Status = BookingStatus.Cancelled;
        return Unit.Value;
    }
}
