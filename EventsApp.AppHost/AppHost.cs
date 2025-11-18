using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var eventApi = builder.AddProject("eventsapi", @"../EventsApi/EventsApi.csproj");

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
