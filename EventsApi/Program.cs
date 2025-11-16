var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Sample events data
var events = new List<Event>
{
    new Event(1, "Tech Conference 2025", "Annual technology conference featuring latest innovations", new DateTime(2025, 3, 15, 9, 0, 0), new DateTime(2025, 3, 17, 18, 0, 0)),
    new Event(2, "Team Building Workshop", "Interactive workshop to strengthen team collaboration", new DateTime(2025, 2, 20, 14, 0, 0), new DateTime(2025, 2, 20, 17, 0, 0)),
    new Event(3, "Product Launch", "Exciting launch event for our new product line", new DateTime(2025, 4, 10, 10, 0, 0), new DateTime(2025, 4, 10, 16, 0, 0)),
    new Event(4, "Training Session", "Professional development training for all staff", new DateTime(2025, 1, 25, 13, 0, 0), new DateTime(2025, 1, 25, 16, 0, 0))
};

app.MapGet("/api/events", () =>
{
    return events.Select(e => new EventDto(e.Id, e.Name, e.Description, e.StartDate, e.EndDate));
})
.WithName("GetEvents");

app.MapGet("/api/events/{id}", (int id) =>
{
    var eventItem = events.FirstOrDefault(e => e.Id == id);
    if (eventItem == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new EventDto(eventItem.Id, eventItem.Name, eventItem.Description, eventItem.StartDate, eventItem.EndDate));
})
.WithName("GetEventById");

app.Run();

record Event(int Id, string Name, string Description, DateTime StartDate, DateTime EndDate);
record EventDto(int Id, string Name, string Description, DateTime StartDate, DateTime EndDate);
