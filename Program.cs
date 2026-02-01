using ConferenceRoom.Api.Data;
using ConferenceRoom.Api.Middleware;
using ConferenceRoom.Api.Models;
using ConferenceRoom.Api.Services.ReservationService;
using ConferenceRoom.Api.Services.RoomService;
using ConferenceRoom.Api.Services.UserService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ConferenceRoomDb")
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReservationService, ReservationService>();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Users.Any())
    {
        var users = new List<UserEntity>
        {
            new UserEntity
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Johnson",
            },
            new UserEntity
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Smith",
            },
        };

        var rooms = new List<RoomEntity>
        {
            new RoomEntity
            {
                Id = 1,
                Name = "Alpha",
                Capacity = 6,
            },
            new RoomEntity
            {
                Id = 2,
                Name = "Beta",
                Capacity = 10,
            },
            new RoomEntity
            {
                Id = 3,
                Name = "Gamma",
                Capacity = 20,
            },
        };

        var reservations = new List<ReservationEntity>
        {
            new ReservationEntity
            {
                Id = 1,
                UserId = 1,
                RoomId = 1,
                StartTime = DateTime.UtcNow.AddHours(1),
                EndTime = DateTime.UtcNow.AddHours(2),
                Status = "Active",
            },
            new ReservationEntity
            {
                Id = 2,
                UserId = 1,
                RoomId = 2,
                StartTime = DateTime.UtcNow.AddHours(3),
                EndTime = DateTime.UtcNow.AddHours(4),
                Status = "Active",
            },
            new ReservationEntity
            {
                Id = 3,
                UserId = 2,
                RoomId = 2,
                StartTime = DateTime.UtcNow.AddHours(1),
                EndTime = DateTime.UtcNow.AddHours(2),
                Status = "Active",
            },
            new ReservationEntity
            {
                Id = 4,
                UserId = 2,
                RoomId = 3,
                StartTime = DateTime.UtcNow.AddHours(5),
                EndTime = DateTime.UtcNow.AddHours(6),
                Status = "Active",
            },
        };

        context.Users.AddRange(users);
        context.Rooms.AddRange(rooms);
        context.Reservations.AddRange(reservations);

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
