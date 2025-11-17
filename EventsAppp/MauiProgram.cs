using Microsoft.Extensions.Logging;
using EventsAppp.Services;
using EventsAppp.ViewModels;
using EventsAppp.Views;

namespace EventsAppp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		builder.Services.AddTransient<IEventService, EventService>();
		
		// Configure HttpClient for API communication
		builder.Services.AddHttpClient<IEventService, EventService>(client =>
		{
			client.BaseAddress = new Uri("http://localhost:5144");
		});

		// Register services and ViewModels
		
		builder.Services.AddTransient<EventsViewModel>();
		builder.Services.AddTransient<EventDetailViewModel>();

		// Register pages
		builder.Services.AddTransient<EventsPage>();
		builder.Services.AddTransient<EventDetailPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
