namespace Workshop.Api.Data.Entities;

public class ReservableObject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ReservableObjectType Type { get; set; }
    public bool IsAvailable { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
}
