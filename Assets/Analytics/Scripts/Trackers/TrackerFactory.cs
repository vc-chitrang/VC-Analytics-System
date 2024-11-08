using System.Collections.Generic;
public class TrackerFactory : FactoryBase {
	public override AnalyticsEvent CreateTracker(string eventName, Dictionary<string, object> eventParams) {
		return new AnalyticsEvent(eventName, eventParams);
	}
}
