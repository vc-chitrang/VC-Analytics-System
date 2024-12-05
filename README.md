# ViitorCloud Analytics

## Project Overview
ViitorCloud Analytics is a Unity-based application designed to provide comprehensive analytics and data visualization tools. The application is structured to facilitate easy integration of various analytics features, making it suitable for developers looking to implement data tracking and reporting in their projects.

## Features
- **Event Tracking**: Log custom events with dynamic data.
- **Screen Tracking**: Track user navigation between screens.
- **User Engagement Metrics**: Track user sessions and engagement duration.
- **Offline Data Storage**: Save data locally and sync when online.
- **Modular Architecture**: Extendable using patterns like Singleton, Factory, and Strategy.

## Design Patterns Used
- **Singleton**: Ensures a single instance of the analytics manager.
- **Observer**: Decouples event generation from event handling.
- **Factory**: Creates different types of analytics events.
- **Command**: Encapsulates tracking actions for batching.
- **Strategy**: Chooses storage methods based on connectivity.
- **Decorator**: Adds optional data processing like encryption.

## Installation
1. Clone the repository to your Unity project:
   ```bash
   git clone https://github.com/your-username/Unity-Custom-Analytics-System.git
   ```

## Integration Steps
1. **Import the Package**:
   - Ensure that the `ViitorCloud Analytics` package is imported into your Unity project.

2. **Using Prefabs**:
   - Drag and drop the `Analytics.prefab` from the `Assets/Analytics/Prefabs/` directory into your scene.
   - This prefab may contain elements that visualize analytics data.

3. **Implementing Trackers**:
   - Use the provided tracker scripts to track various user interactions and data.
   - For example, to track user demographics, instantiate the `UserDemographicProfileTracker` from the `Assets/Analytics/Scripts/Trackers/Demographic-Tracker/` directory.
   - Call the appropriate methods in your game logic to send tracking data to the `AnalyticsManager`.

## Additional Features
### Using VCButton for Tracking Button Clicks
- Instead of using the standard Unity UI Button, you can use the `VCButton` script to track button click counts.
- Attach the `VCButton` script to your button GameObject and configure it in the Inspector to start tracking clicks.

### Tracking Screen Up Time with VCScreenUpTimeTracker
- To track how long a screen is enabled, attach the `VCScreenUpTimeTracker` script to the GameObject representing the screen you want to monitor.
- This script will count how many times the screen has been opened by the user and track the total time it remains active.

### Using VCSessionData for Session Tracking
- The `VCSessionData` script is designed to track session data, including:
  - The number of times the app is opened.
  - Total uptime of the application.
  - Average time spent using the app.
- Integrate this script into your main application logic to gather and analyze session data effectively.

## Example Usage
Here is a simple example of how to use the `AnalyticsManager` and a tracker in your script:

```csharp
using UnityEngine;
using Assets.Analytics.Scripts;

public class ExampleUsage : MonoBehaviour
{
    private AnalyticsManager analyticsManager;

    void Start()
    {
        analyticsManager = FindObjectOfType<AnalyticsManager>();
        analyticsManager.Initialize();
        
        // Example of tracking user interaction
        UserInteractionTracker interactionTracker = new UserInteractionTracker();
        interactionTracker.TrackInteraction("Button Clicked");
    }
}
```

## Conclusion
By following these steps, you can effectively integrate and utilize the ViitorCloud Analytics package in your Unity project. For further customization and advanced features, refer to the individual script documentation within the package.
