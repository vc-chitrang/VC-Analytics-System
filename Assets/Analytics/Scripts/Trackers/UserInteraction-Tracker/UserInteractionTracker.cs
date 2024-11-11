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
}//UserInteractionTracker class end.
