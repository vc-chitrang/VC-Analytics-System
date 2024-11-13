using System.Collections.Generic;
using UnityEngine;

public class UserInteractionTracker{
	private string _eventName;

	public string EventName {
		get => _eventName;
		set => _eventName = value;
	}

	private UserInteractionTracker() {
	}

	public UserInteractionTracker(string EventName) {
		this.EventName = EventName;
	}

	public AnalyticsEvent Create(Dictionary<string, object> eventParams) {
		return new TrackerFactory().CreateTracker(EventName, eventParams);
	}
}//UserInteractionTracker class end.
