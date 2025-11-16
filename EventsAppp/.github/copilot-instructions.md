# Events App - .NET MAUI + Web API Solution

## Project Structure
This solution contains two projects in `mauieventsapp.slnx`:
- **EventsApi/**: ASP.NET Core Web API (.NET 10.0) serving events data via REST endpoints
- **EventsAppp/**: .NET MAUI cross-platform app with MVVM pattern consuming the Events API

## Key Architecture Patterns

### API Development (EventsApi)
- **Minimal API**: Uses `app.MapGet()` style endpoints in `Program.cs` instead of controllers
- **Target Framework**: .NET 10.0 with implicit usings and nullable reference types enabled  
- **Development Server**: Runs on `http://localhost:5144` (configured in `launchSettings.json`)
- **Testing**: Use `EventsApi.http` file for API testing with the `@EventsApi_HostAddress` variable
- **Events Endpoints**: `/api/events` (GET all) and `/api/events/{id}` (GET by ID)
- **Data Models**: Event record with Id, Name, Description, StartDate, EndDate
- **CORS**: Configured to allow all origins for development - restrict in production

### MAUI App (EventsAppp) 
- **MVVM Pattern**: Uses CommunityToolkit.Mvvm with ObservableObject and RelayCommand
- **Shell Navigation**: AppShell.xaml with EventsPage as default, EventDetailPage for navigation
- **Dependency Injection**: Services registered in MauiProgram.cs for ViewModels, Services, and Pages
- **HTTP Client**: Configured with base address pointing to EventsApi (localhost:5144)
- **Platform Targets**: Supports iOS 15.0+, Android 21+, macOS 15.0+, Windows 10.0.17763.0+
- **Key Components**:
  - `EventsPage`: Lists all events with pull-to-refresh
  - `EventDetailPage`: Shows full event details when tapped
  - `EventsViewModel`: Manages events list and navigation commands
  - `EventDetailViewModel`: Handles individual event display with query parameters

## Project-Specific Conventions

### File Organization
- **Models/**: Domain models (Event.cs)
- **Services/**: HTTP services (EventService.cs with IEventService interface)
- **ViewModels/**: MVVM ViewModels using CommunityToolkit.Mvvm
- **Views/**: XAML pages and code-behind
- **Converters/**: Value converters for data binding (IsNotNullConverter, IsNullConverter)

### MVVM Implementation
- ViewModels use `[ObservableProperty]` for properties and `[RelayCommand]` for commands
- ViewModels injected via DI and passed to Views in constructors
- Navigation uses Shell.GoToAsync with query parameters for event details
- Commands follow async/await pattern with proper error handling

## Development Workflow

### Running Both Projects
```bash
# Terminal 1: Start API
cd EventsApi && dotnet run

# Terminal 2: Start MAUI app (macOS)
cd EventsAppp && dotnet run --framework net10.0-maccatalyst

# For other platforms:
# Android: --framework net10.0-android
# iOS: --framework net10.0-ios
# Windows: --framework net10.0-windows
```

### Testing API
Use the `EventsApi.http` file in VS Code with REST Client extension:
- GET all events: `/api/events`
- GET specific event: `/api/events/{id}`
- Returns 404 for non-existent events

### Adding New Features
1. **New API Endpoints**: Add to Program.cs using minimal API pattern
2. **New MAUI Pages**: Create in Views/ folder, register in MauiProgram.cs and AppShell.xaml.cs
3. **New ViewModels**: Inherit from ObservableObject, register in DI container
4. **Navigation**: Register routes in AppShell.xaml.cs, use Shell.GoToAsync() for navigation

## Integration Points
- MAUI app consumes EventsApi via HttpClient configured in MauiProgram.cs
- Event data flows: API → EventService → ViewModel → View via data binding
- Navigation: EventsPage → EventDetailPage with event ID as query parameter
- Error handling: Try-catch in ViewModels with user-friendly DisplayAlert messages

