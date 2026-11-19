using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error BookedByRequired =
        Error.Validation("booking.booked_by_required", "BookedBy is required.");

    public static readonly Error BookedByTooLong =
        Error.Validation("booking.booked_by_too_long", $"BookedBy has at most {Booking.MaxBookedByLength} characters.");

    public static readonly Error EndMustBeAfterStart =
        Error.Validation("booking.end_must_be_after_start", "The end must be strictly after the start.");

    public static readonly Error AttendeesMustBePositive =
        Error.Validation("booking.attendees_must_be_positive", "Attendees must be strictly positive.");

    public static readonly Error AttendeesExceedCapacity =
        Error.Validation("booking.attendees_exceed_capacity", "Attendees cannot exceed the room capacity.");

    public static readonly Error StartInPast =
        Error.Validation("booking.start_in_past", "A booking cannot start in the past.");

    public static readonly Error DurationTooLong =
        Error.Validation("booking.duration_too_long", "A booking lasts at most 4 hours.");

    public static readonly Error RoomOccupied =
        Error.Conflict("booking.room_occupied", "The room already has an active booking in this interval.");

    public static readonly Error AlreadyCancelled =
        Error.Conflict("booking.already_cancelled", "The booking is already cancelled.");

    public static readonly Error NotFound =
        Error.NotFound("booking.not_found", "The booking does not exist.");
}
