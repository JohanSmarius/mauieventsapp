using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var dbServer = builder.AddSqlServer("dbserver")
    .WithHostPort(5000)
    .WithLifetime(ContainerLifetime.Persistent);
var db = dbServer.AddDatabase("EventsDbDirectFeed");

var eventApi = builder.AddProject("eventsapi", @"../EventsApi/EventsApi.csproj")
    .WithReference(db)
    .WaitFor(db);

var publicDevTunnel = builder.AddDevTunnel("devtunnel-public")
    .WithAnonymousAccess()
    .WithReference(eventApi.GetEndpoint("https"));

var mauiApp = builder.AddMauiProject("mauiapp", @"../EventsAppp/EventsAppp.csproj");

mauiApp.AddMacCatalystDevice()
    .WithReference(eventApi);

mauiApp.AddiOSSimulator("iPhone-15-Pro", "990A68A2-90B6-497C-977D-14EA45C707B7")
    .WithOtlpDevTunnel()
    .WithReference(eventApi, publicDevTunnel);

mauiApp.AddAndroidEmulator()
    .WithOtlpDevTunnel() // Required for OpenTelemetry data collection
    .WithReference(eventApi, publicDevTunnel);

builder.Build().Run();
