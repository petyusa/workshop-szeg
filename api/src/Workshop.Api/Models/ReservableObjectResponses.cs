using Workshop.Api.Data.Entities;

namespace Workshop.Api.Models;

public record ReservableObjectResponse(
    int Id,
    string Name,
    ReservableObjectType Type,
    bool IsAvailable,
    int LocationId,
    int? PositionX,
    int? PositionY
);
