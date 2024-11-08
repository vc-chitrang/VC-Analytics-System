using UnityEngine;

public class UserDemographicProfileTracker :TrackerBase, ITracker {
	public string EventName => "User-Demographic-Profile";

	public AnalyticsEvent Create() {
		EventParams.Add("deviceName", SystemInfo.deviceName);
		EventParams.Add("deviceUniqueIdentifier", SystemInfo.deviceUniqueIdentifier);
		EventParams.Add("User Location", "Ahmedabad");

		return new TrackerFactory().CreateTracker(EventName,EventParams);
	}
}
