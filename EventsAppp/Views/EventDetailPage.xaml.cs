using EventsAppp.ViewModels;

namespace EventsAppp.Views;

public partial class EventDetailPage : ContentPage
{
    public EventDetailPage(EventDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}