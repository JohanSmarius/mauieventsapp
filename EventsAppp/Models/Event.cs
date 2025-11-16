namespace EventsAppp.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string DateRange => $"{StartDate:MMM dd, yyyy HH:mm} - {EndDate:MMM dd, yyyy HH:mm}";
    public string DateRangeShort => StartDate.Date == EndDate.Date 
        ? $"{StartDate:MMM dd, yyyy} ({StartDate:HH:mm} - {EndDate:HH:mm})"
        : $"{StartDate:MMM dd} - {EndDate:MMM dd, yyyy}";
}