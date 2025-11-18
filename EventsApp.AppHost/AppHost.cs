using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var eventApi = builder.AddProject("eventsapi", @"../EventsApi/EventsApi.csproj");


builder.Build().Run();
