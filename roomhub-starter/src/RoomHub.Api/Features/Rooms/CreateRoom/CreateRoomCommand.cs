using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed record CreateRoomCommand(string? Name, int Capacity) : ICommand<CreateRoomResponse>;

public sealed record CreateRoomResponse(Guid Id, string Name, int Capacity);
