using Microsoft.EntityFrameworkCore;
using Workshop.Api.Data.Entities;

namespace Workshop.Api.Data;

public class WorkshopDbContext : DbContext
{
    public WorkshopDbContext(DbContextOptions<WorkshopDbContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations => Set<Location>();
    public DbSet<ReservableObject> ReservableObjects => Set<ReservableObject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Location entity
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.IsActive).IsRequired();
        });

        // Configure ReservableObject entity
        modelBuilder.Entity<ReservableObject>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.IsAvailable).IsRequired();
            entity.Property(e => e.LocationId).IsRequired();
            
            entity.HasOne(e => e.Location)
                .WithMany()
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed initial locations
        modelBuilder.Entity<Location>().HasData(
            new Location 
            { 
                Id = 1, 
                Name = "Downtown Location", 
                Address = "123 Main Street, Downtown",
                IsActive = true 
            },
            new Location 
            { 
                Id = 2, 
                Name = "North Branch", 
                Address = "456 North Avenue",
                IsActive = true 
            },
            new Location 
            { 
                Id = 3, 
                Name = "East Side Location", 
                Address = "789 East Boulevard",
                IsActive = true 
            }
        );

        // Seed reservable objects
        var objectId = 1;
        var reservableObjects = new List<ReservableObject>();

        // Downtown Location objects
        for (int i = 1; i <= 5; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Desk {i}01", 
                Type = ReservableObjectType.Desk, 
                IsAvailable = i != 2 && i != 4, // Desk 201 and 401 are reserved
                LocationId = 1 
            });
        }
        for (int i = 1; i <= 3; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Parking A-{i}", 
                Type = ReservableObjectType.ParkingSpace, 
                IsAvailable = i != 2, // Parking A-2 is reserved
                LocationId = 1 
            });
        }

        // North Branch objects
        for (int i = 1; i <= 5; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Desk {i}02", 
                Type = ReservableObjectType.Desk, 
                IsAvailable = i != 1 && i != 3, // Desk 102 and 302 are reserved
                LocationId = 2 
            });
        }
        for (int i = 1; i <= 3; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Parking B-{i}", 
                Type = ReservableObjectType.ParkingSpace, 
                IsAvailable = i != 1, // Parking B-1 is reserved
                LocationId = 2 
            });
        }

        // East Side Location objects
        for (int i = 1; i <= 5; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Desk {i}03", 
                Type = ReservableObjectType.Desk, 
                IsAvailable = i != 5, // Desk 503 is reserved
                LocationId = 3 
            });
        }
        for (int i = 1; i <= 3; i++)
        {
            reservableObjects.Add(new ReservableObject 
            { 
                Id = objectId++, 
                Name = $"Parking C-{i}", 
                Type = ReservableObjectType.ParkingSpace, 
                IsAvailable = true, // All parking available at East Side
                LocationId = 3 
            });
        }

        modelBuilder.Entity<ReservableObject>().HasData(reservableObjects);
    }
}
