using System.Text.Json.Serialization;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Bookings;
using RoomHub.Api.Domain.Common;
using RoomHub.Api.Domain.Rooms;
using RoomHub.Api.Features.Bookings;
using RoomHub.Api.Features.Rooms;
using RoomHub.Api.Infrastructure.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IRoomRepository, InMemoryRoomRepository>();
builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();
builder.Services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
builder.Services.AddHandlers(typeof(Program).Assembly);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapGet("/", () => "RoomHub API is running.");
app.MapRooms();
app.MapBookings();

app.Run();
