using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventsAppp.Models;
using EventsAppp.Services;

namespace EventsAppp.ViewModels;

[QueryProperty(nameof(EventId), "EventId")]
public partial class EventDetailViewModel : ObservableObject
{
    private readonly IEventService _eventService;

    [ObservableProperty]
    private Event? eventItem;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string eventId = string.Empty;

    public EventDetailViewModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    [RelayCommand]
    private async Task LoadEventAsync()
    {
        if (IsLoading || string.IsNullOrWhiteSpace(EventId))
            return;

        try
        {
            IsLoading = true;
            
            if (int.TryParse(EventId, out int id))
            {
                EventItem = await _eventService.GetEventByIdAsync(id);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load event: {ex.Message}", "OK");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    partial void OnEventIdChanged(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _ = LoadEventAsync();
        }
    }
}