using System.Diagnostics;
using EventsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MigrationService;

public class Worker(IServiceProvider serviceProvider, 
    IHostApplicationLifetime hostApplicationLifetime, 
    ILogger<Worker> logger) : BackgroundService
{
    private const string ActivitySourceName = "MigrationService.Worker";
    private static readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting database migration...");

        using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EventsDbContext>();

            logger.LogInformation("Running migrations...");
            await RunMigrationAsync(dbContext, cancellationToken);

            logger.LogInformation("Seeding data...");
            await SeedDataAsync(dbContext, cancellationToken);

            logger.LogInformation("Database migration completed.");
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(EventsDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            //TODO: Only enable the next line if recreation of the database is needed.
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);

            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(EventsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.Events.AnyAsync(cancellationToken))
        {
            return; // Data already seeded
        }

        var events = new List<Event>
        {
            new Event
            {
                Name = ".NET Developer Conference 2025",
                Description = "An great conference focusing on the latest in .NET technologies.",
                StartDate = new DateTime(2026, 11, 24),
                EndDate = new DateTime(2026, 11, 27)
            },
            new Event
            {
                Name = "Music Festival",
                Description = "A three-day outdoor music festival featuring various artists.",
                StartDate = new DateTime(2024, 6, 15),
                EndDate = new DateTime(2024, 6, 17)
            },
            new Event
            {
                Name = "Art Expo",
                Description = "An exhibition showcasing contemporary art from around the world.",
                StartDate = new DateTime(2024, 7, 10),
                EndDate = new DateTime(2024, 7, 12)
            }
        };

        await dbContext.Events.AddRangeAsync(events, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
