using System.Collections.Generic;

public class UserInteractionTracker{
	private string _eventName;

	public string EventName {
		get => _eventName;
		set => _eventName = value;
	}
	
	private UserInteractionTracker() {
	}

	//"EventName"/eventParams: Dictionary<string, object>	
	//[done] "Button_Click"/eventParams: (key:"Play Button", value: 1)	
	//[done] "Screen_View"/eventParams: (key:"Main Menu Screen", value: total Up Time in (DD:MM:YY hh:mm:ss))
	public UserInteractionTracker(string EventName) {
		this.EventName = EventName;
	}

	public AnalyticsEvent Create(Dictionary<string, object> eventParams) {
		return new TrackerFactory().CreateTracker(EventName, eventParams);
	}
}//UserInteractionTracker class end.
