using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(IBookingRepository bookings) : IQueryHandler<GetBookingQuery, GetBookingResponse>
{
    public async Task<Result<GetBookingResponse>> Handle(GetBookingQuery query, CancellationToken cancellationToken)
    {
        var booking = await bookings.GetByIdAsync(query.BookingId, cancellationToken);
        if (booking is null)
            return BookingErrors.NotFound;

        return new GetBookingResponse(
            booking.Id,
            booking.RoomId,
            booking.BookedBy,
            booking.Period.StartUtc,
            booking.Period.EndUtc,
            booking.Attendees,
            booking.Status);
    }
}
