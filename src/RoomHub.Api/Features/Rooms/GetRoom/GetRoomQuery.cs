using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed record GetRoomQuery(Guid RoomId) : IQuery<GetRoomResponse>;

public sealed record GetRoomResponse(Guid Id, string Name, int Capacity);
