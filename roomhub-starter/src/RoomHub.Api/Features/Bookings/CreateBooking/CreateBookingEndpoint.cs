using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public static class CreateBookingEndpoint
{
    public sealed record Request(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);

    public static void Map(RouteGroupBuilder roomBookings) =>
        roomBookings.MapPost("/", Handle).WithName("CreateBooking");

    private static async Task<Results<Created<CreateBookingResponse>, ProblemHttpResult>> Handle(
        Guid roomId,
        Request request,
        ICommandHandler<CreateBookingCommand, CreateBookingResponse> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand(
            roomId,
            request.BookedBy,
            request.StartUtc,
            request.EndUtc,
            request.Attendees);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        return TypedResults.Created($"/api/bookings/{result.Value.Id}", result.Value);
    }
}
