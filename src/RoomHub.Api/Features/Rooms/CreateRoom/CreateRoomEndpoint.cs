using Microsoft.AspNetCore.Http.HttpResults;
using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public static class CreateRoomEndpoint
{
    public sealed record Request(string? Name, int Capacity);

    public static void Map(RouteGroupBuilder group) =>
        group.MapPost("/", Handle).WithName("CreateRoom");

    private static async Task<Results<Created<CreateRoomResponse>, ProblemHttpResult>> Handle(
        Request request,
        ICommandHandler<CreateRoomCommand, CreateRoomResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new CreateRoomCommand(request.Name, request.Capacity), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToProblem();

        return TypedResults.Created($"/api/rooms/{result.Value.Id}", result.Value);
    }
}
