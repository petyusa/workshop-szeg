using Microsoft.EntityFrameworkCore;
using Workshop.Api.Data;
using Workshop.Api.Models;

namespace Workshop.Api.Extensions;

public static class ReservableObjectEndpoints
{
    public static void MapReservableObjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/locations/{locationId}/reservable-objects");

        group.MapGet("", async (int locationId, WorkshopDbContext db) =>
        {
            // Check if location exists
            var locationExists = await db.Locations.AnyAsync(l => l.Id == locationId);
            if (!locationExists)
            {
                return Results.NotFound(new { message = "Location not found" });
            }

            var objects = await db.ReservableObjects
                .Where(o => o.LocationId == locationId)
                .Select(o => new ReservableObjectResponse(
                    o.Id,
                    o.Name,
                    o.Type,
                    o.IsAvailable,
                    o.LocationId,
                    o.PositionX,
                    o.PositionY
                ))
                .ToListAsync();

            return Results.Ok(objects);
        })
        .WithName("GetReservableObjectsByLocation")
        .WithOpenApi();
    }
}
