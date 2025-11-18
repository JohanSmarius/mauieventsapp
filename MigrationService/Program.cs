using EventsApi.Data;
using MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();
builder.AddSqlServerDbContext<EventsDbContext>("EventsDb");

var host = builder.Build();
host.Run();
