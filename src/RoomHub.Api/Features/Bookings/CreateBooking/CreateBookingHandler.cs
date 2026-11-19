using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider time) : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public async Task<Result<CreateBookingResponse>> Handle(
        CreateBookingCommand command,
        CancellationToken cancellationToken)
    {
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return RoomErrors.NotFound;

        var activeBookings = await bookings.GetActiveForRoomAsync(room.Id, cancellationToken);

        var bookingResult = room.Book(
            command.BookedBy,
            command.StartUtc,
            command.EndUtc,
            command.Attendees,
            time.GetUtcNow().UtcDateTime,
            activeBookings);

        if (bookingResult.IsFailure)
            return bookingResult.Error;

        var booking = bookingResult.Value;
        bookings.Add(booking);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBookingResponse(booking.Id, booking.RoomId, booking.Status);
    }
}
