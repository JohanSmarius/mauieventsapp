using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var eventApi = builder.AddProject("eventsapi", @"../EventsApi/EventsApi.csproj");


var mauiApp = builder.AddMauiProject("mauiapp", @"../EventsAppp/EventsAppp.csproj");

mauiApp.AddMacCatalystDevice()
    .WithReference(eventApi);


builder.Build().Run();
