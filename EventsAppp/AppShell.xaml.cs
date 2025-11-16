using EventsAppp.Views;

namespace EventsAppp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		// Register routes for navigation
		Routing.RegisterRoute(nameof(EventDetailPage), typeof(EventDetailPage));
	}
}
