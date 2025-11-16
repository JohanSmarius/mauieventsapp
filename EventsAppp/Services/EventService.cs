using EventsAppp.Models;
using System.Text.Json;

namespace EventsAppp.Services;

public interface IEventService
{
    Task<List<Event>> GetEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
}

public class EventService : IEventService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/events");
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var events = JsonSerializer.Deserialize<List<Event>>(jsonString, _jsonOptions) ?? new List<Event>();
            
            return events;
        }
        catch (Exception ex)
        {
            // In a real app, you'd want proper logging here
            System.Diagnostics.Debug.WriteLine($"Error fetching events: {ex.Message}");
            return new List<Event>();
        }
    }

    public async Task<Event?> GetEventByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/events/{id}");
            response.EnsureSuccessStatusCode();
            
            var jsonString = await response.Content.ReadAsStringAsync();
            var eventItem = JsonSerializer.Deserialize<Event>(jsonString, _jsonOptions);
            
            return eventItem;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching event {id}: {ex.Message}");
            return null;
        }
    }
}