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
		return new TrackerFactory().CreateTracker(EventName, EventParams);
	}

	public void AddButtonClickCount(string parameterKey, int clickCount) {
		EventParams.Clear();
		EventParams.Add(parameterKey, clickCount);		
	}
}//UserInteractionTracker class end.
