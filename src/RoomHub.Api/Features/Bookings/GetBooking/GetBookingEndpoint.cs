using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public static class GetBookingEndpoint
{
    public static void Map(RouteGroupBuilder group) =>
        group.MapGet("/{id:guid}", Handle).WithName("GetBooking");

    private static async Task<Results<Ok<GetBookingResponse>, ProblemHttpResult>> Handle(
        Guid id,
        IQueryHandler<GetBookingQuery, GetBookingResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetBookingQuery(id), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        return TypedResults.Ok(result.Value);
    }
}
