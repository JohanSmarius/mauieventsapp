using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventsAppp.Models;
using EventsAppp.Services;
using System.Collections.ObjectModel;

namespace EventsAppp.ViewModels;

public partial class EventsViewModel : ObservableObject
{
    private readonly IEventService _eventService;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string loadingText = "Loading events...";

    public ObservableCollection<Event> Events { get; } = new();

    public EventsViewModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    [RelayCommand]
    private async Task LoadEventsAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Events.Clear();

            var events = await _eventService.GetEventsAsync();
            
            foreach (var eventItem in events.OrderBy(e => e.StartDate))
            {
                Events.Add(eventItem);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load events: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task GoToEventDetailAsync(Event eventItem)
    {
        if (eventItem == null)
            return;

        await Shell.Current.GoToAsync($"EventDetailPage?EventId={eventItem.Id}");
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadEventsAsync();
    }
}