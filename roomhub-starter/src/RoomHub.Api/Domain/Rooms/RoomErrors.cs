using RoomHub.Api.Domain.Common;

namespace RoomHub.Api.Domain.Rooms;

public static class RoomErrors
{
    public static readonly Error NameRequired =
        Error.Validation("room.name_required", "The room name is required.");

    public static readonly Error NameTooLong =
        Error.Validation("room.name_too_long", $"The room name has at most {Room.MaxNameLength} characters.");

    public static readonly Error CapacityMustBePositive =
        Error.Validation("room.capacity_must_be_positive", "The room capacity must be strictly positive.");

    public static readonly Error NotFound =
        Error.NotFound("room.not_found", "The room does not exist.");
}
