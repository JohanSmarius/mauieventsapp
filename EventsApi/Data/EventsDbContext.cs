using Microsoft.EntityFrameworkCore;

namespace EventsApi.Data;

public class EventsDbContext : DbContext
{
    public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial data
        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Name = "Tech Conference 2025",
                Description = "Annual technology conference featuring latest innovations",
                StartDate = new DateTime(2025, 3, 15, 9, 0, 0),
                EndDate = new DateTime(2025, 3, 17, 18, 0, 0)
            },
            new Event
            {
                Id = 2,
                Name = "Team Building Workshop",
                Description = "Interactive workshop to strengthen team collaboration",
                StartDate = new DateTime(2025, 2, 20, 14, 0, 0),
                EndDate = new DateTime(2025, 2, 20, 17, 0, 0)
            },
            new Event
            {
                Id = 3,
                Name = "Product Launch",
                Description = "Exciting launch event for our new product line",
                StartDate = new DateTime(2025, 4, 10, 10, 0, 0),
                EndDate = new DateTime(2025, 4, 10, 16, 0, 0)
            },
            new Event
            {
                Id = 4,
                Name = "Training Session",
                Description = "Professional development training for all staff",
                StartDate = new DateTime(2025, 1, 25, 13, 0, 0),
                EndDate = new DateTime(2025, 1, 25, 16, 0, 0)
            }
        );
    }
}
