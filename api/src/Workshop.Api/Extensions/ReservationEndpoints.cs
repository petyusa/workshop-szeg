using Microsoft.EntityFrameworkCore;
using Workshop.Api.Data;
using Workshop.Api.Data.Entities;
using Workshop.Api.Middleware;
using Workshop.Api.Models;

namespace Workshop.Api.Extensions;

public static class ReservationEndpoints
{
    public static void MapReservationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reservations");

        group.MapPost("/", async (CreateReservationRequest request, WorkshopDbContext db, HttpContext context) =>
        {
            var user = context.GetCurrentUser();
            if (user is null)
            {
                return Results.Unauthorized();
            }

            // Validate dates
            if (request.EndDateTime <= request.StartDateTime)
            {
                return Results.BadRequest("End date must be after start date");
            }

            // Check if object exists
            var reservableObject = await db.ReservableObjects
                .FirstOrDefaultAsync(o => o.Id == request.ReservableObjectId);

            if (reservableObject is null)
            {
                return Results.NotFound("Reservable object not found");
            }

            // Check for conflicts
            var hasConflict = await db.Reservations
                .AnyAsync(r => r.ReservableObjectId == request.ReservableObjectId &&
                              r.StartDateTime < request.EndDateTime &&
                              r.EndDateTime > request.StartDateTime);

            if (hasConflict)
            {
                return Results.BadRequest("This object is already reserved for the selected time period");
            }

            var reservation = new Reservation
            {
                ReservableObjectId = request.ReservableObjectId,
                UserId = user.Username,
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                CreatedAt = DateTime.UtcNow
            };

            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();

            var response = new ReservationResponse(
                reservation.Id,
                reservation.ReservableObjectId,
                reservableObject.Name,
                reservableObject.Type.ToString(),
                reservation.StartDateTime,
                reservation.EndDateTime,
                reservation.CreatedAt
            );

            return Results.Created($"/api/reservations/{reservation.Id}", response);
        })
        .WithName("CreateReservation")
        .WithOpenApi();

        group.MapGet("/my", async (WorkshopDbContext db, HttpContext context) =>
        {
            var user = context.GetCurrentUser();
            if (user is null)
            {
                return Results.Unauthorized();
            }

            var reservations = await db.Reservations
                .Where(r => r.UserId == user.Username)
                .Include(r => r.ReservableObject)
                .OrderByDescending(r => r.StartDateTime)
                .Select(r => new ReservationResponse(
                    r.Id,
                    r.ReservableObjectId,
                    r.ReservableObject!.Name,
                    r.ReservableObject.Type.ToString(),
                    r.StartDateTime,
                    r.EndDateTime,
                    r.CreatedAt
                ))
                .ToListAsync();

            return Results.Ok(reservations);
        })
        .WithName("GetMyReservations")
        .WithOpenApi();

        group.MapGet("/object/{objectId}/check", async (int objectId, DateTime start, DateTime end, WorkshopDbContext db) =>
        {
            if (end <= start)
            {
                return Results.BadRequest("End date must be after start date");
            }

            var hasConflict = await db.Reservations
                .AnyAsync(r => r.ReservableObjectId == objectId &&
                              r.StartDateTime < end &&
                              r.EndDateTime > start);

            return Results.Ok(new { Available = !hasConflict });
        })
        .WithName("CheckAvailability")
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id, WorkshopDbContext db, HttpContext context) =>
        {
            var user = context.GetCurrentUser();
            if (user is null)
            {
                return Results.Unauthorized();
            }

            var reservation = await db.Reservations
                .FirstOrDefaultAsync(r => r.Id == id && r.UserId == user.Username);

            if (reservation is null)
            {
                return Results.NotFound();
            }

            db.Reservations.Remove(reservation);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteReservation")
        .WithOpenApi();
    }
}
