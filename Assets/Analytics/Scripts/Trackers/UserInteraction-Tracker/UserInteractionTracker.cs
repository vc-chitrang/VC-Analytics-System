using UnityEngine;

public class UserInteractionTracker : TrackerBase, ITracker {
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

	public AnalyticsEvent Create() {
		return new TrackerFactory().CreateTracker(_eventName, EventParams);
	}

	public void Add(string key, object value) {
		if (EventParams.ContainsKey(key)) {
			EventParams[key] = value;
		} else {
			EventParams.Add(key, value);
		}
	}

	public void AddButtonClickCount(string key) {
		if (EventParams.ContainsKey(key)) {
			// Ensure the value is an integer before attempting to increment
			if (EventParams[key] is int currentValue) {
				EventParams[key] = currentValue + 1;
			} else {
				// Handle cases where the existing value isn't an integer
				Debug.LogWarning($"Value for '{key}' is not an integer and cannot be incremented.");
			}
		} else {
			EventParams.Add(key, 1);
		}
	}
}//UserInteractionTracker class end.
