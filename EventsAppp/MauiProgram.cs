using Microsoft.Extensions.Logging;
using EventsAppp.Services;
using EventsAppp.ViewModels;
using EventsAppp.Views;
using Microsoft.Extensions.Hosting;

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

		builder.AddServiceDefaults();
	
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
