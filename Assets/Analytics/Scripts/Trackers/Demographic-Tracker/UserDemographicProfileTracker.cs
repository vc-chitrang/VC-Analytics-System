using System.Collections.Generic;
using UnityEngine;

public class UserDemographicProfileTracker :TrackerBase, ITracker {
	public string EventName => "User-Demographic-Profile";

	public AnalyticsEvent Create() {
		Dictionary<string, object> eventParams = new Dictionary<string, object>(){
			{"deviceName", SystemInfo.deviceName},
			{"deviceUniqueIdentifier", SystemInfo.deviceUniqueIdentifier},
			{"User Location", "Ahmedabad"}
		};

		return new TrackerFactory().CreateTracker(EventName,eventParams);
	}
}