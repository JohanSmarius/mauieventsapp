using EventsApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add EF Core In-Memory database
builder.AddSqlServerDbContext<EventsDbContext>("EventsDbDirectFeed");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EventsDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.MapGet("/api/events", async (EventsDbContext db) =>
{
    var events = await db.Events.ToListAsync();
    return events.Select(e => new EventDto(e.Id, e.Name, e.Description, e.StartDate, e.EndDate));
})
.WithName("GetEvents");

app.MapGet("/api/events/{id}", async (int id, EventsDbContext db) =>
{
    var eventItem = await db.Events.FindAsync(id);
    if (eventItem == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new EventDto(eventItem.Id, eventItem.Name, eventItem.Description, eventItem.StartDate, eventItem.EndDate));
})
.WithName("GetEventById");

app.Run();

record EventDto(int Id, string Name, string Description, DateTime StartDate, DateTime EndDate);
