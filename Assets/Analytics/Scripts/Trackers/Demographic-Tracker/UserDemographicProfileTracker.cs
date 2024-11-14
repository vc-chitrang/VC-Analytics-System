using System.Collections.Generic;
using UnityEngine;

public class UserDemographicProfileTracker :TrackerBase, ITracker {
	public string EventName => "User-Demographic-Profile";

	public AnalyticsEvent Create() {
		//User ID: Unique identifier for each user. 
		//User Device ID: Unique device identifier (e.g., SystemInfo.deviceUniqueIdentifier). 
		//User Age, Gender, and Location (if applicable): For analyzing demographics. 
		//User Segment: Group or type of user (e.g., new user, returning user). 
		Dictionary<string, object> eventParams = new Dictionary<string, object>();		
		eventParams.Add("User Device ID", SystemInfo.deviceUniqueIdentifier);
		eventParams.Add("Location", "Unknown");

		return new TrackerFactory().CreateTracker(EventName,eventParams);
	}
}